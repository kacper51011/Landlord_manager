import { Meta, StoryObj } from "@storybook/react";
import { StatisticsCard } from "./StatisticsCard";
import { FaUserFriends } from "react-icons/fa";

const meta = {
  title: "Example/StaticsCard",
  component: StatisticsCard,
  // This component will have an automatically generated Autodocs entry: https://storybook.js.org/docs/react/writing-docs/autodocs
  tags: ["autodocs"],
  parameters: {
    // More on how to position stories at: https://storybook.js.org/docs/react/configure/story-layout
    layout: "centered",
  },
} satisfies Meta<typeof StatisticsCard>;
export default meta;
type Story = StoryObj<typeof meta>;

export const Light: Story = {
  args: {
    mode: "light",
    label: "Light mode",
    colors: "red",
    value: 100,
    Icon: FaUserFriends,
  },
};

export const Dark: Story = {
  args: {
    mode: "dark",
    label: "Dark mode",
    colors: "red",
    value: 100,
    Icon: FaUserFriends,
  },
};
