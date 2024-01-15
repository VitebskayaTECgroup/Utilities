import * as React from "react";
import { styled } from "@mui/material/styles";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell, { tableCellClasses } from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import { Button, CircularProgress, Typography } from "@mui/material";
import showPeriodOfWork from "../../../Functions/showPeriodTime";

function ArchiveTable(props) {
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


  return (
    <>
      {props.dataTable.length === 0 ? (
        <CircularProgress />
      ) : (
        <TableContainer sx={{ maxHeight: 500 }} component={Paper}>
          <Table stickyHeader size="small" aria-label="a dense table">
            <TableHead>
              <TableRow>
                <StyledTableCell align="center">Объект</StyledTableCell>
                <StyledTableCell align="left">Дата и время</StyledTableCell>
                <StyledTableCell align="left">Cостояние</StyledTableCell>
                <StyledTableCell align="left">Оператор</StyledTableCell>
                <StyledTableCell align="left">
                  Сводная информация
                </StyledTableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {props.dataTable.map((row) => {
                return (
                  row.info.length !== 0 && (
                    <StyledTableRow key={row.objectId}>
                      <StyledTableCell
                        sx={{ verticalAlign: "baseline" }}
                        align="center"
                        component="th"
                        scope="row"
                      >
                        <Button variant="text">{row.objectId}</Button>
                      </StyledTableCell>
                      <StyledTableCell align="left">
                        {row.info.map((info) => {
                          return (
                            <Typography
                              variant="caption"
                              display="block"
                              gutterBottom
                              key={info.Id}
                            >
                              {info.Date}
                            </Typography>
                          );
                        })}
                      </StyledTableCell>
                      <StyledTableCell align="left">
                        {row.info.map((info) => {
                          return (
                            <Typography
                              variant="caption"
                              display="block"
                              gutterBottom
                              key={info.Id}
                            >
                              {info.State}
                            </Typography>
                          );
                        })}
                      </StyledTableCell>
                      <StyledTableCell align="left">
                        {row.info.map((info) => {
                          return (
                            <Typography
                              variant="caption"
                              display="block"
                              gutterBottom
                              key={info.Id}
                            >
                              {info.Operator}
                            </Typography>
                          );
                        })}
                      </StyledTableCell>
                      <StyledTableCell
                        sx={{ verticalAlign: "baseline" }}
                        align="left"
                      >
                        <Table>
                          <TableHead>
                            <TableRow>
                              <StyledTableCell
                                align="left"
                                sx={{ width: "100px" }}
                              >
                                Состояние
                              </StyledTableCell>
                              <StyledTableCell
                                align="left"
                                sx={{ width: "100px" }}
                              >
                                Суммарное время
                              </StyledTableCell>
                            </TableRow>
                          </TableHead>
                          <TableBody>
                            {row.periods.map((period) => {
                              return (
                                <TableRow key={period.Key}>
                                  <StyledTableCell align="left">
                                    <Typography variant="caption">
                                      {period.Key}
                                    </Typography>
                                  </StyledTableCell>
                                  <StyledTableCell align="left">
                                    <Typography variant="caption">
                                      {showPeriodOfWork(period.Value)}
                                    </Typography>
                                  </StyledTableCell>
                                </TableRow>
                              );
                            })}
                          </TableBody>
                        </Table>
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
export default ArchiveTable;
