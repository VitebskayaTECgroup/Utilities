import * as React from "react";
import { styled } from "@mui/material/styles";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell, { tableCellClasses } from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import { Button, CircularProgress } from "@mui/material";
import allStates, { colorForState } from "../../../AllStates/allStates";

function ArchiveGraph(props) {
  const StyledTableCell = styled(TableCell)(({ theme }) => ({
    [`&.${tableCellClasses.head}`]: {
      backgroundColor: theme.palette.common.black,
      color: theme.palette.common.white,
      padding: "3px",
    },
    [`&.${tableCellClasses.body}`]: {
      fontSize: 14,
      padding: "3px",
      border: 0,
    },
  }));

  const StyledTableRow = styled(TableRow)(({ theme }) => ({
    "&:nth-of-type(odd)": {
      backgroundColor: theme.palette.action.focus,
    },
    "&:last-child td, &:last-child th": {
      border: 0,
    },
  }));

  function setColorForCurrentState(currentState) {
    const colorForErrorInCode = "#FFFFFF"; // При какой-либо ошибке (к примеру, если в allStates указано больше состояний, чем в colorForState)

    const stateIndex = allStates.findIndex((state) => state === currentState);
    const color = colorForState[stateIndex]
      ? colorForState[stateIndex]
      : colorForErrorInCode;

    return stateIndex === -1 ? colorForErrorInCode : color;
  }

  return (
    <>
      {props.dataGraph.length === 0 ? (
        <CircularProgress />
      ) : (
        <TableContainer sx={{ maxHeight: 500 }} component={Paper}>
          <Table stickyHeader size="small" aria-label="a dense table">
            <TableHead>
              <TableRow>
                <StyledTableCell align="center" sx={{ maxWidth: 30 }}>
                  Объект
                </StyledTableCell>
                <StyledTableCell align="left">
                  График изменений и состояний
                </StyledTableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {props.dataGraph.map((row) => {
                return (
                  row.info.length !== 0 && (
                    <StyledTableRow key={row.objectId}>
                      <StyledTableCell
                        align="center"
                        component="th"
                        scope="row"
                        sx={{ maxWidth: 30 }}
                      >
                        <Button variant="text" sx={{ padding: 0 }}>
                          {row.objectId}
                        </Button>
                      </StyledTableCell>
                      <StyledTableCell align="left" sx={{ display: "flex" }}>
                        {row.info.map((info) => {
                          return (
                            <div
                              key={info.Id}
                              style={{
                                maxWidth: `${info.Percent}%`,
                                width: `${info.Percent}%`,

                                height: 20,
                                backgroundColor: setColorForCurrentState(
                                  info.State
                                ),
                              }}
                              title={`${info.State} с ${info.Date}`}
                            ></div>
                          );
                        })}
                      </StyledTableCell>
                    </StyledTableRow>
                  )
                );
              })}
            </TableBody>
          </Table>
        </TableContainer>
      )}
    </>
  );
}
export default ArchiveGraph;
