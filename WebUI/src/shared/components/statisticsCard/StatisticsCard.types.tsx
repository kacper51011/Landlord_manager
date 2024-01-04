import { IconType } from "react-icons";

export type StatisticsCardType = {
  label: string;
  Icon: IconType;
  colors: "red" | "blue" | "green" | "yellow";
  value: number;
  mode?: "light" | "dark";
};
