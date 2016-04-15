var output = './public';

module.exports = {
  entry: {
    'bundle': './app/app.js'
  },

  output: {
    path: output,
    filename: '[name].js'
  },

  resolve: {
    extensions: ['', '.js', '.json'],
  },

  module: {
    loaders: [
      { test: /\.js/, loader: 'babel', exclude: /node_modules/ }
    ]
  },

};
