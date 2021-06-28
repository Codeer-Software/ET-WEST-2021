using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Windows.Data;
using System.Windows.Threading;
using Socket.Io.Client.Core;

namespace etwest.Models
{
    public class Client
    {
        public ObservableCollection<Data> Data { get; private set; }
        public ObservableCollection<Log> Logs { get; private set; }

        public void Init()
        {
            Data = new ObservableCollection<Data>();
            Logs = new ObservableCollection<Log>();
            BindingOperations.EnableCollectionSynchronization(Data, new object());

            var client = new SocketIoClient();
            client.Events.OnConnect.Subscribe(_ =>
            {
                client.Emit("init");
            });
            client.On("history")
                .Select(value =>
                {
                    var data = new List<Data>();
                    foreach (var v in value.Data[0].EnumerateArray())
                    {
                        var x = v.GetProperty("x").GetString();
                        var y = v.GetProperty("y").GetDouble();
                        data.Add(new Data {DateTime = DateTime.Parse(x), Lux = y});
                    }

                    return data;
                })
                .SubscribeOnApp()
                .Subscribe(values =>
                {
                    foreach (var v in values)
                    {
                        Data.Add(v);
                    }
                });
            client.On("data")
                .Select(value =>
                {
                    var lux = value.Data[0].GetProperty("lux");
                    var x = lux.GetProperty("x").GetString();
                    var y = lux.GetProperty("y").GetDouble();
                    return new Data {DateTime = DateTime.Parse(x), Lux = y};
                })
                .SubscribeOnApp()
                .Subscribe(value =>
                {
                    Data.Add(value);
                    while (Data.Count > 0 && Data[0].DateTime < (DateTime.Now - TimeSpan.FromSeconds(500)))
                    {
                        Data.RemoveAt(0);
                    }
                });
            client.OpenAsync(new Uri(App.EndPoint));
        }
    }

    public static class ObservableEx2
    {
        public static IObservable<T> SubscribeOnApp<T>(this IObservable<T> source)
        {
            var subject = new Subject<T>();
            var dispatcher = App.Current.Dispatcher;
            source.Subscribe(value => dispatcher.BeginInvoke(() =>
            {
                subject.OnNext(value);
            }), exception => dispatcher.BeginInvoke(() =>
            {
                subject.OnError(exception);
            }), () => dispatcher.BeginInvoke(() =>
             {
                 subject.OnCompleted();
             }));
            return subject;
        }
    }

    public class Data
    {
        public DateTime DateTime { get; set; }
        public double Lux { get; set; }
    }
}
