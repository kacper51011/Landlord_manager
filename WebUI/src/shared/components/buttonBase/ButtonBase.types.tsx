import { ButtonHTMLAttributes, ReactNode } from "react";

export type ButtonBaseProps = {
  children?: ReactNode;
  onClick: () => void;
  label?: string;
  size?: "large" | "medium" | "small";
  borders?: "sharp" | "rounded";
  mode?: "light" | "dark";
} & ButtonHTMLAttributes<HTMLButtonElement>;
