import { useState } from "react";
import { NavbarRowProps } from "./NavbarRow.types";
import "./NavbarRow.css";
import { FaChevronDown, FaChevronUp } from "react-icons/fa";
import { NavbarRowItem } from "../NavbarRowItem/NavbarRowItem";

export const NavbarRow = ({ label, Icon, links }: NavbarRowProps) => {
  const [isOpen, setOpen] = useState(false);

  const SetIsClicked = (): void => {
    setOpen(!isOpen);
  };

  return (
    <div className="Navbar-row-container">
      <button className="Navbar-row-button" onClick={SetIsClicked}>
        <Icon />
        <label className="Navbar-row-label">{label}</label>
        {isOpen ? <FaChevronUp /> : <FaChevronDown />}
        {/* icon changing depens on isOpen or not */}
      </button>
      {isOpen && links.map((link) => <NavbarRowItem link={link.link} itemLabel={link.name} />)}
    </div>
  );
};
