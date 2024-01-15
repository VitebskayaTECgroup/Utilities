import {
  Box,
  CircularProgress,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
} from "@mui/material";
import { useState } from "react";
import showPeriodOfWork from "../../../Functions/showPeriodTime";
import { useStartTimer } from "../../../Functions/startTimer";

function DialogSummary(props) {
  const TIME_UPDATE_STATES = 300000;

  const [dataSummary, setDataSummary] = useState([]);
  const [showSpinner, setShowSpinner] = useState(true);

  const dataTag = props.currentDialogsTag;

  let host = 'http://web.vst.vitebsk.energo.net/mnemo/api/'
  try {
    if (!process.env.NODE_ENV || process.env.NODE_ENV === 'development') {
      host = 'http://localhost:62902/'
    } else {
        // production code
    }
  } catch (e) {}

  useStartTimer(
    updateSummary,
    `${host}states/GetStatesSummary?IdState=${dataTag.Id}&dateFrom=${props.dateBegin}&dateTo=${props.dateEnd}`,
    updateSummary,
    `${host}states/GetStatesSummary?IdState=${dataTag.Id}&dateFrom=${props.dateBegin}&dateTo=${props.dateEnd}`,
    TIME_UPDATE_STATES,
    props.immediatelyUpdateSummary
  );

  function updateSummary(data) {
    setShowSpinner(false);
    setDataSummary(data);
  }

  return (
    <>
      <TableContainer>
        <Table size="small" aria-label="a dense table">
          <TableHead>
            <TableRow>
              <TableCell sx={{ fontWeight: 600 }}>Состояние</TableCell>
              <TableCell sx={{ fontWeight: 600 }} align="right">
                Всего
              </TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {dataSummary.map((item) => (
              <TableRow
                key={item.Value}
                sx={{
                  "&:last-child td, &:last-child th": {
                    border: 0,
                    fontSize: "0.9em",
                  },
                }}
              >
                <TableCell component="th" scope="row">
                  {item.Value}
                </TableCell>
                <TableCell align="right">{showPeriodOfWork(item.TotalDiffSpan)}</TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
      <Box
        sx={{
          display: `${showSpinner ? "flex" : "none"}`,
          justifyContent: "center",
        }}
      >
        <CircularProgress />
      </Box>
    </>
  );
}
export default DialogSummary;
