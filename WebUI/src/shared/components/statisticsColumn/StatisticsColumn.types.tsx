export type StatisticsColumnsType = {
  list?: Item[];
  colors?: "blue" | "red" | "green";
  mode?: "light" | "dark";
  visible: boolean;
};

type Item = {
  value: number;
  date: string;
};
