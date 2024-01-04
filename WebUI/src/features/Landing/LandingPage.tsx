//do zrobienia: dodać listę obiektów i przemapować sobie li
import { IoBarChartOutline } from "react-icons/io5";
import { IoIosCheckmark } from "react-icons/io";
import { AiOutlineStock } from "react-icons/ai";
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
        <div className="font-poppins font-extrabold text-landingMain text-2xl cursor-default">Landlord Manager</div>
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
          <div className=" mb-4 bg-landingGreen text-sm text-white px-4 py-2 rounded-full font-poppins cursor-default">
            Home
          </div>
          <h1 className="mb-2 font-poppins font-extrabold text-3xl leading-10 text-landingMain">
            Managing private estates has <br /> never been so easy
          </h1>
          <p className="mb-2 font-poppins font-medium text-xl leading-8 text-landingMain">
            Unlock the future of estate management services <br /> with our cutting edge solutions
          </p>
          {homeSectionList.map((item) => (
            <div className="flex items-center mb-4 ">
              <div className="bg-landingGreen  text-white rounded-full w-4 h-4 mr-4">
                <IoIosCheckmark />
              </div>
              <p className="text-landingMain font-poppins text-xl">{item}</p>
            </div>
          ))}
          <div className="flex mt-4">
            <button className="bg-landingGreen text-white px-4 py-2 rounded-lg font-poppins mr-8">Get Started</button>
            <button className="font-poppins font-semibold px-4 py-2  text-landingMain rounded-lg border-2">
              Login
            </button>
          </div>
        </div>
        <img
          src="landing-photo1.jpg"
          alt="Middle aged man sitting in apartment"
          className="w-[600px] h-[450px] rounded-md mt-4"
        />
      </section>
      <section className="flex flex-col justify-center items-center w-screen mt-8 mb-16">
        <div className="rounded-xl bg-landingLightGreen text-landingGreen px-2 py-1 font-poppins mb-8">Features</div>
        <h2 className=" font-poppins font-bold text-4xl text-landingMain mb-4">
          Unlock the Future of Management Services
        </h2>
        <p className="font-poppins font-medium text-xl text-landingMain text-center mb-20">
          We are on a mission to revolutionize the way you manage your estate, communicate with your tenants
          <br /> and meet the deadlines. With a commitment to innovation and user empowerment
        </p>
        {/* columns */}
        <div className="flex flex-row justify-center items-center w-screen">
          {/* first col */}
          <div className="flex flex-col justify-around items-center w-1/3">
            <div className="bg-landingGreen text-white px-4 py-4 text-lg rounded-md mb-2">
              <IoBarChartOutline />
            </div>
            <p className="font-poppins font-bold text-sm text-landingMain mb-2">
              <span className="text-landingGreen">Innovative</span> estate management
            </p>
            <p className="text-landingMain text-sm font-poppins text-center mb-32">
              Discover a world where managing your estates is effortless.
              <br /> Our user-friendly platform allows you to track finances,
              <br /> manage apartments, and gain insights into your tenants life.
            </p>
            <div className="bg-landingGreen text-white px-4 py-4 text-lg rounded-md mb-2">
              <AiOutlineStock />
            </div>
            <p className="font-poppins font-bold text-sm text-landingMain mb-2">
              <span className="text-landingGreen">Innovative</span> estate management
            </p>
            <p className="text-landingMain text-sm font-poppins text-center mb-32">
              Discover a world where managing your estates is effortless.
              <br /> Our user-friendly platform allows you to track finances,
              <br /> manage apartments, and gain insights into your tenants life.
            </p>
          </div>
          {/* second col */}
          <img src="landing-photo2.jpg" className="w-[400px] h-[550px] rounded-lg" />
          {/* third col */}
          <div className="flex flex-col justify-around items-center w-1/3">
            <div className="bg-landingGreen text-white px-4 py-4 text-lg rounded-md mb-2">
              <IoBarChartOutline />
            </div>
            <p className="font-poppins font-bold text-sm text-landingMain mb-2">
              <span className="text-landingGreen">Innovative</span> estate management
            </p>
            <p className="text-landingMain text-sm font-poppins text-center mb-32">
              Discover a world where managing your estates is effortless.
              <br /> Our user-friendly platform allows you to track finances,
              <br /> manage apartments, and gain insights into your tenants life.
            </p>
            <div className="bg-landingGreen text-white px-4 py-4 text-lg rounded-md mb-2">
              <IoBarChartOutline />
            </div>
            <p className="font-poppins font-bold text-sm text-landingMain mb-2">
              <span className="text-landingGreen">Innovative</span> estate management
            </p>
            <p className="text-landingMain text-sm font-poppins text-center mb-32">
              Discover a world where managing your estates is effortless.
              <br /> Our user-friendly platform allows you to track finances,
              <br /> manage apartments, and gain insights into your tenants life.
            </p>
          </div>
        </div>
      </section>
      <section className=" w-screen flex justify-center mb-16">
        <div className="w-2/3 bg-[#084162] h-72 flex justify-around items-center">
          <div className="flex flex-col">
            <p className="text-[#C4C4C4] font-poppins  font-bold text-2xl">
              The fastest way from
              <br /> idea to finance solution
            </p>
            <p className="font-poppins text-[#C4C4C4] font-normal text-lg">
              Join us today and take care
              <br /> of your financial future
            </p>
          </div>
          <div>
            <button className="p-3 bg-landingGreen text-white rounded-md">Get Started</button>
            <button>Learn More</button>
          </div>
        </div>
      </section>
    </div>
  );
};
