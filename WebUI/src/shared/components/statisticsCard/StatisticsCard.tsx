import { StatisticsCardType } from "./StatisticsCard.types";
import "./StatisticsCard.css";
export const StatisticsCard = ({ label, Icon, value, mode, colors }: StatisticsCardType) => {
  return (
    <div className={`stat-card-container stat-card-container-${mode}-${colors}`}>
      <label className={`stat-card-label stat-card-${mode}`}>{label}</label>
      <div className="stat-card-row">
        <Icon className={`stat-card-icon stat-card-${mode}`} />
        <div className={`stat-card-value stat-card-${mode}`}>{value}</div>
      </div>
    </div>
  );
};
