import { IconType } from "react-icons";

export type NavbarRowProps = {
  label: string;
  Icon: IconType;
  links: NavbarRowLink[];
};

type NavbarRowLink = {
  name: string;
  link: string;
};
