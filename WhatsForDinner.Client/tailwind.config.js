/** @type {import('tailwindcss').Config} */
export default {
  content: [
    './index.html',
    './src/**/*.{js,vue,ts,jsx,tsx}'
  ],
  theme: {
    extend: {
      colors: {
        'green-bright': '#8BE06D',
        'green-darker': '#15992A',
        'main-font': '#411827',
        'another-dish-bg-button': '#D64747',
        'filter-bg': '#FCEBDE',
        'filter-question-bg': '#FFE2E2',
        'filter-clear': '#F9D5D5',
        'filter-border': '#864A4A',
        'filter-V-bg': '#B4DD9A',
        'filter-V-text': '#15992A',
        'filter-X': '#FF8989',
        'table-border':'#864A4A',
        'tag-color':'#15992A'
      },

    }

  },
  plugins: [
    // eslint-disable-next-line no-undef
    require('@tailwindcss/forms'),
    // ...
  ],
}

