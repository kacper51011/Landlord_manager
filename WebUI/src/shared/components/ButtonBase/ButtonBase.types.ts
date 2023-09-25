enum ButtonSize {
  SMALL,
  MEDIUM,
  LARGE,
}

const SIZE_MAPS: Record<ButtonSize, string> = {
  [ButtonSize.SMALL]: "px-2.5 py-0.5 text-xs",
  [ButtonSize.MEDIUM]: "px-3 py-0.7 text-xs",
  [ButtonSize.LARGE]: "px-4 py-1 text-sm",
};

enum ButtonRadius {
  SHARP,
  ROUNDED,
}
const RADIUS_MAPS: Record<ButtonRadius, string> = {
  [ButtonRadius.SHARP]: "px-2.5 text-xs",
  [ButtonRadius.ROUNDED]: "px-2.5 text-xs",
};
