/** @type {import('tailwindcss').Config} */
export default {
  content: ["./index.html", "./src/**/**/**/**.{js,ts,jsx,tsx}"],
  theme: {
    extend: {
      colors: {
        main: "#E0F2FE",
        secondary: "#38BDF8",
        third: "#0EA5E9",
        text: "#111827",
      },
    },
  },
  plugins: [],
};
