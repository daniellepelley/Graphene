var output = './Graphene.GraphiQL/public';

module.exports = {
  entry: {
    'bundle': './Graphene.GraphiQL/app/app.js'
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
