const fs = require('fs').promises;
const express = require('express');
const app = express();
const http = require('http').Server(app);
const io = require('socket.io')(http, {
  cors: {
    origin: "*",
    methods: ["GET", "POST"],
  },
  allowEIO3: true,
});

const history = [];
const data = {};

app.get('/api/put', function (req, res) {
  const { lux } = req.query;
  data.lux = { x: new Date(), y: Number(lux) };
  history.push(data.lux);
  while (history.length > 100) {
    history.shift();
  }
  io.emit('data', data);
  console.log(data);
  res.sendStatus(200);
});
app.use('/', express.static("public"));

io.on('connection', (socket) => {
  socket.on('init', () => {
    socket.emit('history', history);
  });
  socket.on('history', () => {
    socket.emit('history', history);
  });
});

http.listen(process.env.PORT || 3000, '0.0.0.0');
