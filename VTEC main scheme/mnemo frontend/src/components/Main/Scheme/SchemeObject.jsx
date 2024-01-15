import { Menu, MenuItem } from "@mui/material";
import * as React from "react";
import { useState } from "react";
import styles from "./Scheme.module.css";
import allStates from "../../AllStates/allStates";

function SchemeObject(props) {
  const data = props.schemeObjectData;
  const dataStyle = JSON.parse(data.Style);
  const inlineStyles = dataStyle.inlineStyles;
  const className = dataStyle.className;

  const [anchorEl, setAnchorEl] = useState(null);
  const open = Boolean(anchorEl);

  const handleClick = (event) => {
    event.preventDefault();
    setAnchorEl(event.currentTarget);
  };

  const handleClose = (event) => {
    setAnchorEl(null);

    if (event.target.role === "menuitem") {
      setLog(props.schemeObjectData.Id, event.target.innerText);
    }
  };

  let host = 'http://web.vst.vitebsk.energo.net/mnemo/api/'
  try {
    if (!process.env.NODE_ENV || process.env.NODE_ENV === 'development') {
      host = 'http://localhost:62902/'
    } else {
        // production code
    }
  } catch (e) {}

  function setLog(tagId, state) {
    const path = `${host}logs/SetLogs`;

    let formData = new FormData();
    formData.append("TagId", tagId);
    formData.append("State", state);

    fetch(path, {
      method: "POST",
      body: formData,
      credentials: "include",
    }).then(() => {
      props.updateTags();
    });
  }

  return (
    <>
      <div>
        {data.ShowMenu && (
          <Menu anchorEl={anchorEl} open={open} onClose={handleClose}>
            <MenuItem
              disabled
              divider
              sx={{
                fontWeight: 700,
                opacity: "inherit !important",
              }}
            >{`${data.Type} ${data.Description} : ${data.ActiveState.Value}`}</MenuItem>
            {allStates.map((state) => {
              return (
                <MenuItem
                  key={state}
                  onClick={handleClose}
                  selected={state === props.schemeObjectData.ActiveState.Value}
                >
                  {state}
                </MenuItem>
              );
            })}
          </Menu>
        )}
      </div>
      <img
        onContextMenu={handleClick}
        className={styles[className]}
        style={inlineStyles}
        src={
          data.ActiveState.Value !== ""
            ? require(`../../../images/${className}/${data.ActiveState.Value}.png`)
            : null
        }
        title={`${data.Type} ${data.Description}: ${data.ActiveState.Value}`}
        onDoubleClick={() => {
          props.hiddenDialogs();
          props.changeCurrentDialogsTag(data);
        }}
        alt=""
      />
    </>
  );
}
export default SchemeObject;

/*
const obj = {
    className: "kotel", 
    inlineStyles: {top: "15%", left: "15%" }
} 

Select * From dbo.Tags
Where id = 'SN-2'
Update dbo.Tags set Style = '{
    "className": "nasos", 
    "inlineStyles": {"top": "88%", "left": "34%" }
}' Where id = 'SN-2'

// web\sqlexpress
// nasos, kotel, turbina, roy, regulator 
*/
