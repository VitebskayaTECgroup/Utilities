import { Box, CircularProgress } from "@mui/material";
import { useState } from "react";
import checkIsNeedLoadDataOnScroll from "../../../Functions/loadDataOnScroll";
import showPeriodOfWork from "../../../Functions/showPeriodTime";
import { useStartTimer } from "../../../Functions/startTimer";
import styles from "./Dialogs.module.css";

function DialogStates(props) {
  const TIME_UPDATE_STATES = 300000;
  const ADD_STATES = 100;

  const dataTag = props.currentDialogsTag;

  const [dataStates, setDataStates] = useState([]);
  const [countAddStates, setCountAddStates] = useState(ADD_STATES);
  const [isLoadedDataOnPage, setIsLoadedDataOnPage] = useState(false);
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
    loadNewStates,
    `${host}states/GetStates?IdState=${
      dataTag.Id
    }&countStates=${
      props.isUpdateData ? ADD_STATES : countAddStates
    }&skipStates=${
      props.isUpdateData ? 0 : countAddStates - ADD_STATES
    }&dateFrom=${props.dateBegin}&dateTo=${props.dateEnd}`,
    updateStates,
    `${host}states/GetStates?IdState=${dataTag.Id}&countStates=${countAddStates}&dateFrom=${props.dateBegin}&dateTo=${props.dateEnd}`,
    TIME_UPDATE_STATES,
    props.immediatelyUpdateStates
  );

  function loadNewStates(data) {
    if (props.isUpdateData) {
      setCountAddStates(ADD_STATES);
      setDataStates(data);
      props.doNotUpdateData();
      setShowSpinner(false);
    } else {
      setDataStates(dataStates.concat(data));
      setShowSpinner(false);
    }
  }

  function updateStates(data) {
    setDataStates(data);
  }

  function loadStatesOnScroll() {
    if (isLoadedDataOnPage || countAddStates > dataStates.length) {
      return;
    }
    setShowSpinner(true);
    setIsLoadedDataOnPage(true);
    setTimeout(() => {
      props.doNotUpdateData();
      const NEW_COUNT_ADD_DATA = countAddStates + ADD_STATES;
      setCountAddStates(NEW_COUNT_ADD_DATA);
      setIsLoadedDataOnPage(false);
    }, 250);
  }

  return (
    <>
      <div className={styles.dialogsTitleTableData}>
        <div>Состояние</div>
        <div>Автор</div>
        <div>Дата</div>
        <div>Всего</div>
      </div>
      <div
        style={{
          overflowY: "auto",
          maxHeight: "40vh",
          marginBottom: 5,
          minHeight: 200,
        }}
        onScroll={(e) => checkIsNeedLoadDataOnScroll(e, loadStatesOnScroll)}
      >
        {dataStates.map((state) => {
          return (
            <div className={styles.dialogsTableData} key={state.Id}>
              <div>{state.Value}</div>
              <div>{state.Operator}</div>
              <div>{state.Date}</div>
              <div>{showPeriodOfWork(state.DiffSpan)}</div>
            </div>
          );
        })}
        <Box
          sx={{
            display: `${showSpinner ? "flex" : "none"}`,
            justifyContent: "center",
          }}
        >
          <CircularProgress />
        </Box>
      </div>
    </>
  );
}
export default DialogStates;
