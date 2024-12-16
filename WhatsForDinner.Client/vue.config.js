module.exports = {
  devServer: {
    proxy: {
      '/api': {
        target: 'http://localhost:5262', // Адрес ASP.NET Core API
        changeOrigin: true
      }
    }
  }
};