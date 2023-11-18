import { Meta, StoryObj } from "@storybook/react";
import { ButtonBase } from "./ButtonBase";

const meta = {
  title: "Example/ButtonBase",
  component: ButtonBase,
  // This component will have an automatically generated Autodocs entry: https://storybook.js.org/docs/react/writing-docs/autodocs
  tags: ["autodocs"],
  parameters: {
    // More on how to position stories at: https://storybook.js.org/docs/react/configure/story-layout
    layout: "centered",
  },
} satisfies Meta<typeof ButtonBase>;
export default meta;
type Story = StoryObj<typeof meta>;

export const WithoutProps: Story = {
  args: {
    label: "No props",
  },
};

export const Light: Story = {
  args: {
    mode: "light",
    label: "Light",
    borders: "rounded",
    size: "medium",
  },
};

export const Dark: Story = {
  args: {
    mode: "dark",
    label: "Dark",
    borders: "rounded",
    size: "medium",
  },
};

export const Large: Story = {
  args: {
    mode: "light",
    label: "ButtonBase",
    borders: "rounded",
    size: "large",
  },
};
