import styles from "./Logs.module.css";

function Log(props) {
  return (
    <div className={styles.blocks}>
      <div className={styles.blockData}>{props.dataLog.Date}</div>
      <div className={styles.blockOperator}>{props.dataLog.Operator}</div>
      <div className={styles.blockEvent}>{props.dataLog.Text}</div>
    </div>
  );
}
export default Log;
