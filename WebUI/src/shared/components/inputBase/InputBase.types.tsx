import { ComponentProps } from "react";

export type InputBaseProps = {
  placeholder: string;
  size?: "large" | "medium" | "small";
  borders?: "sharp" | "rounded";
  mode?: "light" | "dark";
} & Omit<ComponentProps<"input">, "size" | "borders" | "mode" | "placeholder">;
