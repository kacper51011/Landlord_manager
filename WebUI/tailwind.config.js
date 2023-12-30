/** @type {import('tailwindcss').Config} */
export default {
  content: ["./index.html", "./src/**/**/**/**.{js,ts,jsx,tsx}"],
  theme: {
    extend: {
      colors: {
        landingMain: "#084162",
        landingGreen: "#22C55E",
        landingGrey: "#0EA5E9",
        text: "#111827",
      },
      fontFamily: {
        inter: ["Inter"],
        poppins: ["Poppins"],
      },
    },
  },
  plugins: [],
};
