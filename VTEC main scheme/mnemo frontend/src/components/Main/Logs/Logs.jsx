import React, { useState } from "react";
import { useStartTimer } from "../../Functions/startTimer";
import Log from "./Log";
import styles from "./Logs.module.css";
import { styled } from "@mui/material/styles";
import Paper from "@mui/material/Paper";
import { Box, CircularProgress, IconButton } from "@mui/material";
import CloseIcon from "@mui/icons-material/Close";
import checkIsNeedLoadDataOnScroll from "../../Functions/loadDataOnScroll";

function Logs(props) {
  const ADD_SOME_NUMBER_LOGS = 100;
  const TIME_UPDATE_LOGS = 300000;
  const [dataLogs, setDataLogs] = useState([]);
  const [isLoadedDataOnPage, setIsLoadedDataOnPage] = useState(false);
  const [countAddLogs, setCountAddLogs] = useState(ADD_SOME_NUMBER_LOGS);
  const [showSpinner, setShowSpinner] = useState(true);

  let host = 'http://web.vst.vitebsk.energo.net/mnemo/api/'
  try {
    if (!process.env.NODE_ENV || process.env.NODE_ENV === 'development') {
      host = 'http://localhost:62902/'
    } else {
        // production code
    }
  } catch (e) {}

  useStartTimer(
    loadNewLogs,
    `${host}logs/GetLogs?countLogs=${countAddLogs}&skipLogs=${
      countAddLogs - ADD_SOME_NUMBER_LOGS
    }`,
    updateLogs,
    `${host}logs/GetLogs?countLogs=${countAddLogs}&skipLogs=${
      countAddLogs - ADD_SOME_NUMBER_LOGS
    }`,
    TIME_UPDATE_LOGS,
    countAddLogs
  );

  function loadNewLogs(data) {
    setDataLogs(dataLogs.concat(data));
    setShowSpinner(false);
  }

  function updateLogs(data) {
    setDataLogs(data);
  }

  function loadLogsOnScroll() {
    if (isLoadedDataOnPage || countAddLogs > dataLogs.length) {
      return;
    }
    setShowSpinner(true);
    setIsLoadedDataOnPage(true);
    setTimeout(() => {
      const NEW_COUNT_ADD_DATA = countAddLogs + ADD_SOME_NUMBER_LOGS;

      setCountAddLogs(NEW_COUNT_ADD_DATA);
      setIsLoadedDataOnPage(false);
    }, 250);
  }

  const StyledTitleTable = styled("div")(({ theme }) => ({
    backgroundColor:
      props.colorSchemeFon === "black"
        ? theme.palette.divider
        : theme.palette.common.black,
    color: theme.palette.common.white,
  }));

  const StyledCLoseIcon = styled(CloseIcon)(({ theme }) => ({
    color: theme.palette.common.white,
  }));

  return (
    <Paper sx={{ overflow: "hidden" }}>
      <StyledTitleTable className={styles.logsTitleTable}>
        <div className={styles.blockData}>Дата</div>
        <div className={styles.blockOperator}>Оператор</div>
        <div className={styles.blockEvent}>Событие</div>
      </StyledTitleTable>
      <IconButton
        onClick={props.createLogs}
        sx={{
          position: "absolute",
          right: "2%",
          top: "-0.7%",
        }}
      >
        <StyledCLoseIcon />
      </IconButton>
      <div
        style={{ maxHeight: 600, overflowY: "scroll" }}
        onScroll={(e) => checkIsNeedLoadDataOnScroll(e, loadLogsOnScroll)}
      >
        {Boolean(dataLogs) &&
          dataLogs.map(
            (dataLog, index) =>
              index <= countAddLogs && (
                <Log dataLog={dataLog} key={dataLog.Id} />
              )
          )}
        <Box
          sx={{
            display: `${showSpinner ? "flex" : "none"}`,
            justifyContent: "center",
          }}
        >
          <CircularProgress />
        </Box>
      </div>
    </Paper>
  );
}
export default Logs;
