"use strict";

var _express = _interopRequireDefault(require("express"));

var _routes = _interopRequireDefault(require("./routes"));

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

// Initialize app.
const app = (0, _express.default)(); // Configuration

app.use(cors());
app.use(_express.default.json());
app.use(_express.default.urlencoded({
  extended: true
})); // Routes.

app.get('/', (req, res) => res.send('<p>👋 Xin chào</p>'));
app.use('/api', _routes.default); // Start server.

app.listen(config.port);