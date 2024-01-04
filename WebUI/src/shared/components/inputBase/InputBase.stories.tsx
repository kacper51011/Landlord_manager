import { Meta, StoryObj } from "@storybook/react";
import { InputBase } from "./InputBase";

const meta = {
  title: "Example/InputBase",
  component: InputBase,
  // This component will have an automatically generated Autodocs entry: https://storybook.js.org/docs/react/writing-docs/autodocs
  tags: ["autodocs"],
  parameters: {
    // More on how to position stories at: https://storybook.js.org/docs/react/configure/story-layout
    layout: "centered",
  },
} satisfies Meta<typeof InputBase>;
export default meta;
type Story = StoryObj<typeof meta>;

export const WithoutProps: Story = {
  args: {
    placeholder: "Default",
  },
};

export const Light: Story = {
  args: {
    mode: "light",
    placeholder: "Light",
    borders: "rounded",
    size: "medium",
  },
};

export const Dark: Story = {
  args: {
    mode: "dark",
    placeholder: "Dark",
    borders: "rounded",
    size: "medium",
  },
};

export const Large: Story = {
  args: {
    mode: "light",
    placeholder: "ButtonBase",
    borders: "rounded",
    size: "large",
  },
};

export const Small: Story = {
  args: {
    mode: "light",
    placeholder: "ButtonBase",
    borders: "rounded",
    size: "small",
  },
};
