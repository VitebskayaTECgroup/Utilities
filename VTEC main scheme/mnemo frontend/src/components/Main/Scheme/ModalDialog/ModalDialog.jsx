import React, { useEffect, useState } from "react";
import Draggable from "react-draggable";
import DialogStates from "./DialogStates";
import DialogSummary from "./DialogSummary";

import Button from "@mui/material/Button";
import Dialog from "@mui/material/Dialog";
import DialogContent from "@mui/material/DialogContent";
import DialogTitle from "@mui/material/DialogTitle";
import { IconButton, Paper, Stack, TextField } from "@mui/material";
import CloseIcon from "@mui/icons-material/Close";

function PaperComponent(props) {
  return (
    <Draggable
      handle="#draggable-dialog-title"
      cancel={'[class*="MuiDialogContent-root"]'}
    >
      <Paper sx={{ minWidth: 750 }} {...props} />
    </Draggable>
  );
}

function ModalDialog(props) {
  const [showSummary, setShowSummary] = useState(false);
  const [immediatelyUpdateStates, setImmediatelyUpdateStates] = useState(0);
  const [isUpdateDataStates, setIsUpdateDataStates] = useState(true);
  const [immediatelyUpdateSummary, setImmediatelyUpdateSummary] = useState(0);

  const dataTag = props.currentDialogsTag;

  const [dateFrom, setDateFrom] = useState("");
  const [timeFrom, setTimeFrom] = useState("00:00");
  const [dateTo, setDateTo] = useState("");
  const [timeTo, setTimeTo] = useState("23:59");

  const [dateBegin, setDateBegin] = useState("");
  const [dateEnd, setDateEnd] = useState("");

  function showListTable() {
    let minDate = dateFrom !== "" ? dateFrom : "1753-02-02";
    let maxDate = dateTo !== "" ? dateTo : "9999-12-12";

    const dateBegin = `${minDate} ${timeFrom}`;
    const dateEnd = `${maxDate} ${timeTo}`;

    setShowSummary(false);

    setDateBegin(dateBegin);
    setDateEnd(dateEnd);
    setImmediatelyUpdateStates(immediatelyUpdateStates + 1);
    setIsUpdateDataStates(true);
  }

  function showSummaryTable() {
    let minDate = dateFrom !== "" ? dateFrom : "1753-02-02";
    let maxDate = dateTo !== "" ? dateTo : "9999-12-12";

    const dateBegin = `${minDate} ${timeFrom}`;
    const dateEnd = `${maxDate} ${timeTo}`;

    setShowSummary(true);

    setDateBegin(dateBegin);
    setDateEnd(dateEnd);
    setImmediatelyUpdateSummary(immediatelyUpdateSummary + 1);
  }

  function doNotUpdateDataStates() {
    setIsUpdateDataStates(false);
  }

  return (
    <>
      <div>
        <Dialog
          open={props.showDialogs}
          onClose={props.hiddenDialogs}
          PaperComponent={PaperComponent}
          aria-labelledby="draggable-dialog-title"
        >
          <DialogTitle
            style={{
              cursor: "move",
            }}
            id="draggable-dialog-title"
          >
            <IconButton
              onClick={props.hiddenDialogs}
              sx={{ position: "absolute", right: "2%" }}
            >
              <CloseIcon />
            </IconButton>
            <Stack
              direction="row"
              justifyContent="center"
              alignItems="center"
              spacing={0}
            >
              {`${dataTag.Type} ${dataTag.Description}`}
            </Stack>

            <Stack
              direction="row"
              justifyContent="center"
              alignItems="center"
              spacing={1}
            >
              <Button
                variant={`${!showSummary ? "contained" : "outlined"}`}
                onClick={showListTable}
                size="small"
              >
                Показать таблицу состояний
              </Button>
              <Button
                variant={`${showSummary ? "contained" : "outlined"}`}
                onClick={showSummaryTable}
                size="small"
              >
                Показать суммарное время
              </Button>
            </Stack>
          </DialogTitle>
          <Stack
            direction="row"
            justifyContent="center"
            alignItems="center"
            spacing={1}
          >
            <TextField
              id="dateFrom"
              label="Начальная дата"
              type="date"
              value={dateFrom}
              onChange={(e) => setDateFrom(e.target.value)}
              sx={{ width: 150 }}
              InputLabelProps={{
                shrink: true,
              }}
            />
            <TextField
              id="timeFrom"
              label="Начальное время"
              type="time"
              value={timeFrom}
              onChange={(e) => setTimeFrom(e.target.value)}
              sx={{ width: 150 }}
              InputLabelProps={{
                shrink: true,
              }}
            />
            <TextField
              id="dateTo"
              label="Конечная дата"
              type="date"
              value={dateTo}
              onChange={(e) => setDateTo(e.target.value)}
              sx={{ width: 150 }}
              InputLabelProps={{
                shrink: true,
              }}
            />
            <TextField
              id="timeTo"
              label="Конечное время"
              type="time"
              value={timeTo}
              onChange={(e) => setTimeTo(e.target.value)}
              sx={{ width: 150 }}
              InputLabelProps={{
                shrink: true,
              }}
            />
          </Stack>
          <DialogContent sx={{ maxHeight: 500 }}>
            {!showSummary ? (
              <DialogStates
                currentDialogsTag={dataTag}
                dateBegin={dateBegin}
                dateEnd={dateEnd}
                immediatelyUpdateStates={immediatelyUpdateStates}
                isUpdateData={isUpdateDataStates}
                doNotUpdateData={doNotUpdateDataStates}
              />
            ) : (
              <DialogSummary
                currentDialogsTag={dataTag}
                dateBegin={dateBegin}
                dateEnd={dateEnd}
                immediatelyUpdateSummary={immediatelyUpdateSummary}
              />
            )}
          </DialogContent>
        </Dialog>
      </div>
    </>
  );
}
export default ModalDialog;
