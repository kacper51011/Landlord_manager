import { Meta, StoryObj } from "@storybook/react";
import { NavbarRow } from "./NavbarRow";
import { FaUserFriends } from "react-icons/fa";

const meta = {
  title: "Example/NavbarRow",
  component: NavbarRow,
  // This component will have an automatically generated Autodocs entry: https://storybook.js.org/docs/react/writing-docs/autodocs
  tags: ["autodocs"],
  parameters: {
    // More on how to position stories at: https://storybook.js.org/docs/react/configure/story-layout
    layout: "centered",
  },
} satisfies Meta<typeof NavbarRow>;
export default meta;
type Story = StoryObj<typeof meta>;

export const WithoutProps: Story = {
  args: {
    label: "Category",
    Icon: FaUserFriends,
    links: [
      { name: "asd", link: "asd" },
      { name: "qwe", link: "asd" },
    ],
  },
};
