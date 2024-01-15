import styles from "./Main.module.css";
import React, { useState } from "react";
import Scheme from "./Scheme/Scheme";
import Logs from "./Logs/Logs";
import { ThemeProvider, createTheme } from "@mui/material/styles";
import { Dialog, DialogContent, DialogContentText, DialogTitle, Paper } from "@mui/material";
import { grey } from "@mui/material/colors";

function Main() {
  const [createHelper, setCreateHelper] = useState(false);
  const [showArchive, setShowArchive] = useState(false);
  const [createElementLogs, setCreateElementLogs] = useState(false);
  const [modeTheme, setMode] = useState(
    localStorage.getItem("colorTheme") === "dark" ? "dark" : "light"
  );
  const [colorSchemeFon, setColorSchemeFon] = useState(
    localStorage.getItem("colorTheme") === "dark" ? "black" : "white"
  );

  function hiddenArchive() {
    setShowArchive(!showArchive);
  }

  function createLogs() {
    setCreateElementLogs(!createElementLogs);
  }

  function changeColorTheme() {
    const newColorSchemeFon = colorSchemeFon === "black" ? "white" : "black";
    const newModeTheme = modeTheme === "dark" ? "light" : "dark";
    setColorSchemeFon(newColorSchemeFon);
    setMode(modeTheme === "dark" ? "light" : "dark");
    localStorage.setItem("colorTheme", newModeTheme);
  }

  const theme = createTheme(
    modeTheme === "dark"
      ? {
          palette: {
            mode: modeTheme,
          },
        }
      : {
          palette: {
            mode: modeTheme,
            background: { paper: grey[200] },
          },
        }
  );

  return (
    <ThemeProvider theme={theme}>
      <Paper
        sx={{
          color: `${colorSchemeFon === "black" ? "#fff" : "#000"}`,
          position: "relative",
        }}
      >
        <div className={styles.archiveButtons}>
          <div onClick={() => setShowArchive(!showArchive)}>Архив</div>
          <div onClick={changeColorTheme}>Изменить Тему</div>
          <div onClick={() => setCreateHelper(true)}>Справка</div>
        </div>
        <div className={styles.archiveButtons}>
          <div className={styles.logsButton} onClick={createLogs}>
            События
          </div>
        </div>
        <Scheme
          colorSchemeFon={colorSchemeFon}
          showArchive={showArchive}
          hiddenArchive={hiddenArchive}
        />
        <Dialog
          open={createHelper}
          onClose={() => setCreateHelper(false)}
          maxWidth="lg"
        >
          <DialogTitle>{"Чтобы получить состояние объекта, кликните на него левой кнопкой мыши два раза"}</DialogTitle>
          <DialogTitle>{"Чтобы изменить состояние объекта, кликните на него правой кнопкой мыши"}</DialogTitle>
        </Dialog>
        <Dialog
          fullWidth
          open={createElementLogs}
          onClose={createLogs}
          maxWidth="lg"
        >
          <Logs
            createElementLogs={createElementLogs}
            colorSchemeFon={colorSchemeFon}
            createLogs={createLogs}
          />
        </Dialog>
      </Paper>
    </ThemeProvider>
  );
}
export default Main;
