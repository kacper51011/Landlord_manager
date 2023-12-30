//do zrobienia: dodać listę obiektów i przemapować sobie li

import { IoIosCheckmark, IoIosCheckmarkCircle } from "react-icons/io";
import { NavList } from "./LandingPage.types";

const list: NavList[] = [
  { name: "Features", link: "/LandingPage/features" },
  { name: "About", link: "/LandingPage/about" },
  { name: "FAQ", link: "/LandingPage/faq" },
  { name: "Contact us", link: "/LandingPage/contact" },
];

const homeSectionList: string[] = [
  "Easy estate management",
  "Centralized communication with tenants",
  "Payment reminders",
  "24/7 Customer support",
];

export const LandingPage = () => {
  return (
    <div className="bg-slate-50">
      <nav className="flex flex-row justify-between items-center w-screen px-11 py-6">
        <div className="font-poppins font-extrabold text-landingMain text-2xl">Landlord Manager</div>
        <ul className="flex justify-between">
          {list.map((item) => (
            <li className="mx-10">
              <a className="font-inter font-semibold text-landingMain  text-lg" href={item.link}>
                {item.name}
              </a>
            </li>
          ))}
        </ul>
        <div className="flex justify-between">
          <button className="font-poppins font-semibold text-landingMain mr-6">Login</button>
          <button className="bg-landingGreen text-white px-4 py-2 rounded-lg font-poppins">Register</button>
        </div>
      </nav>
      <section className="py-8 flex flex-row justify-around items-center">
        <div className="flex flex-col items-start">
          <div className=" mb-4 bg-landingGreen text-sm text-white px-4 py-2 rounded-full font-poppins">Home</div>
          <h1 className="mb-2 font-poppins font-extrabold text-3xl leading-10 text-landingMain">
            Managing private estates has <br /> never been so easy
          </h1>
          <p className="mb-2 font-poppins font-medium text-xl leading-8 text-landingMain">
            Unlock the future of estate management services <br /> with our cutting edge solutions
          </p>
          {homeSectionList.map((item) => (
            <div className="flex items-center mt-4 ">
              <div className="bg-landingGreen  text-white rounded-full w-4 h-4 mr-4">
                <IoIosCheckmark />
              </div>
              <p className="text-landingMain font-poppins text-xl">{item}</p>
            </div>
          ))}
        </div>
        <img
          src="public/landing-photo1.jpg"
          alt="Middle aged man sitting in apartment"
          className="w-[600px] h-96 rounded-md mt-4"
        />
      </section>
    </div>
  );
};
