import "./InputBase.css";
import { InputBaseProps } from "./InputBase.types";

export const InputBase = ({
  placeholder,
  mode = "light",
  size = "medium",
  borders = "sharp",
  ...props
}: InputBaseProps) => {
  return (
    <input className={`input-${mode} input-${size} input-${borders}`} placeholder={placeholder} {...props}></input>
  );
};
