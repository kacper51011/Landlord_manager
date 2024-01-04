import "./NavbarRowItem.css";

type Props = {
  itemLabel: string;
  link: string;
};

// póki co używany anchor, do zmiany z Linkiem
export const NavbarRowItem = ({ link, itemLabel }: Props) => {
  return (
    <a href={link} className="Navbar-row-item">
      <label className="Navbar-row-item-label">{itemLabel}</label>
    </a>
  );
};
