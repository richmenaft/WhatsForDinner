/* eslint-env node */

module.exports = {
  root: true,
  plugins: ['@typescript-eslint'],
  extends: [
    'plugin:vue/recommended',
    'eslint:recommended',
    'plugin:@typescript-eslint/recommended',
    '@vue/eslint-config-prettier/skip-formatting',
  ],
  parser: 'vue-eslint-parser',
  parserOptions: {
    ecmaVersion: 'latest',
    parser: "@typescript-eslint/parser",
    sourceType:"module"
  },

  overrides: [
    {
      files: ['postcss.config.js', 'tailwindcss.config.js', 'vue.config.js'],
      env: {
        node: true
      }
    }
  ]
}
