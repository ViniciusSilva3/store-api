import express from 'express';

// Initialize app.
const app = express();

// Routes.
app.get('/', (req, res) => res.send('<p>👋 Xin chào</p>'));
app.use('/api', apiRoute);

// Start server.
app.listen(config.port);
