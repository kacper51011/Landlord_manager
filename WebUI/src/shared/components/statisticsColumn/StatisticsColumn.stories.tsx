import { Meta, StoryObj } from "@storybook/react";
import { StatisticsColumns } from "./StatisticsColumn";

const meta = {
  title: "Example/StaticsColumn",
  component: StatisticsColumns,
  // This component will have an automatically generated Autodocs entry: https://storybook.js.org/docs/react/writing-docs/autodocs
  tags: ["autodocs"],
  parameters: {
    // More on how to position stories at: https://storybook.js.org/docs/react/configure/story-layout
    layout: "centered",
  },
} satisfies Meta<typeof StatisticsColumns>;
export default meta;
type Story = StoryObj<typeof meta>;

export const Light: Story = {
  args: {
    mode: "light",
    list: [
      { value: 10, date: "16:00" },
      { value: 20, date: "17:00" },
      { value: 30, date: "18:00" },
      { value: 40, date: "19:00" },
      { value: 50, date: "20:00" },
      { value: 60, date: "21:00" },
      { value: 70, date: "22:00" },
    ],
    visible: true,
    colors: "blue",
  },
};

export const Dark: Story = {
  args: {
    mode: "dark",
    list: [
      { value: 10, date: "16:00" },
      { value: 20, date: "17:00" },
      { value: 30, date: "18:00" },
      { value: 40, date: "19:00" },
      { value: 50, date: "20:00" },
      { value: 60, date: "21:00" },
      { value: 70, date: "22:00" },
    ],
    visible: true,
    colors: "red",
  },
};
