import styles from "./Scheme.module.css";
import fonBlack from "../../../images/fon_black.jpg";
import fonWhite from "../../../images/fon_mister_white.jpg";
import { useRef, useState } from "react";
import SchemeObject from "./SchemeObject";
import { useStartTimer } from "../../Functions/startTimer";
import { Paper } from "@mui/material";
import ModalDialog from "./ModalDialog/ModalDialog";
import Archive from "./Archive/Archive";

function Scheme(props) {
  const TIME_UPDATE_TAGS = 300000;
  const [showDialogs, setShowDialogs] = useState(false);
  const [dataSchemeObjects, setDataSchemeObjects] = useState([]);
  const [currentDialogsTag, setCurrentDialogsTag] = useState("");
  const [needUpdate, setNeedUpdate] = useState(0);
  const exceptions = ["RU_13/1.2-0.2"];

  const mainDiv = useRef(null);

  let host = 'http://web.vst.vitebsk.energo.net/mnemo/api/'
  try {
    if (!process.env.NODE_ENV || process.env.NODE_ENV === 'development') {
      host = 'http://localhost:62902/'
    } else {
        // production code
    }
  } catch (e) {}

  useStartTimer(
    loadTags,
    host + "tags/GetTags",
    loadTags,
    host + "tags/GetTags",
    TIME_UPDATE_TAGS,
    needUpdate
  );

  function loadTags(data) {
    setDataSchemeObjects(data);
  }

  function updateTags() {
    setNeedUpdate(needUpdate + 1);
  }

  function hiddenDialogs() {
    setShowDialogs(!showDialogs);
    setCurrentDialogsTag("");
  }

  function changeCurrentDialogsTag(newId) {
    setCurrentDialogsTag(newId);
  }

  return (
    <>
      {props.showArchive && (
        <Archive
          showArchive={props.showArchive}
          hiddenArchive={props.hiddenArchive}
          dataSchemeObjects={dataSchemeObjects}
        />
      )}
      <Paper
        className={styles.fon}
        style={{
          background: `${props.colorSchemeFon === "black" ? "#000" : "#fff"}`,
        }}
        square
      >
        <div className={styles.schemeImg} style={{ position: "relative" }}>
          <div ref={mainDiv}>
            {dataSchemeObjects.map(
              (schemeObjectData) =>
                exceptions.find((id) => id !== schemeObjectData.Id) && (
                  <SchemeObject
                    hiddenDialogs={hiddenDialogs}
                    changeCurrentDialogsTag={changeCurrentDialogsTag}
                    schemeObjectData={schemeObjectData}
                    updateTags={updateTags}
                    key={schemeObjectData.Id}
                  />
                )
            )}
          </div>
          {currentDialogsTag && (
            <ModalDialog
              hiddenDialogs={hiddenDialogs}
              showDialogs={showDialogs}
              currentDialogsTag={currentDialogsTag}
            />
          )}
          <img
            className={styles.schemeImg}
            src={props.colorSchemeFon === "black" ? fonBlack : fonWhite}
            alt=""
          />
        </div>
      </Paper>
    </>
  );
}
export default Scheme;
