import "./StatisticsColumn.css";
import { StatisticsColumnsType } from "./StatisticsColumn.types";

export const StatisticsColumns = ({
  list = [
    { value: 20, date: "dupa" },
    { value: 60, date: "sraka" },
  ],
  mode = "light",
  colors,
  visible = true,
}: StatisticsColumnsType) => {
  return (
    <div className={`stat-col-container stat-col-container-${colors}`}>
      {list.map((item) => (
        <div className="stat-col-item-container">
          <div className={`stat-col stat-col-${mode}-${colors}`} style={{ height: `${item.value}%` }} />
          <label className={`stat-col-label`}>{item.date}</label>
        </div>
      ))}
    </div>
  );
};
