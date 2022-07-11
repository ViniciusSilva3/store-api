import express from 'express';
import cors from 'cors';

import apiRouter from './routes';

// Initialize app.
const app = express();

// Configuration
app.use(cors());
app.use(express.json());
app.use(express.urlencoded({ extended: true }));

// Routes.
app.get('/', (req, res) => res.send('<p>👋 Xin chào</p>'));
app.use('/api', apiRouter);

// Start server.
app.listen(3000);
