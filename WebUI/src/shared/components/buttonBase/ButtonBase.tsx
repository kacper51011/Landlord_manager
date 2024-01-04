import { ButtonBaseProps } from "./ButtonBase.types";
import "./ButtonBase.css";

export const ButtonBase = ({
  children,
  size = "medium",
  borders = "rounded",
  mode = "light",
  label,
  ...props
}: ButtonBaseProps) => {
  return (
    <button className={`button-${mode} button-${size} button-${borders}`} {...props}>
      {children || label}
    </button>
  );
};
