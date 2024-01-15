import {
  Button,
  Checkbox,
  Dialog,
  FormControl,
  FormControlLabel,
  FormGroup,
  InputLabel,
  Paper,
  Select,
  TextField,
  Typography,
} from "@mui/material";
import { Stack } from "@mui/system";
import { useCallback, useState } from "react";
import styles from "./Archive.module.css";
import ArchiveGraph from "./ArchiveGraph";
import ArchiveList from "./ArchiveTable";
import allStates from "../../../AllStates/allStates";

function Archive(props) {
  const [isShowArchiveList, setIsShowArchiveList] = useState(false);
  const [isShowArchiveGraph, setIsShowArchiveGraph] = useState(false);
  const [isWrongDate, setIsWrongDate] = useState(false);
  const [dataArchive, setDataArchive] = useState([]);

  const [inputDates, setInputDates] = useState({
    dateFrom: "",
    timeFrom: "00:00",
    dateTo: "",
    timeTo: "23:59",
  });

  const [inputChanges, setInputChanges] = useState({
    "С изменениями": true,
  });

  const [inputTypes, setInputTypes] = useState({
    Котел: true,
    Насос: true,
    Регулятор: true,
    РОУ: true,
    Турбина: true,
  });

  const [inputStates, setInputStates] = useState({
    "В горячем резерве": true,
    "В холодном резерве": true,
    "В капитальном ремонте": true,
    "В текущем ремонте": true,
    "В работе": true,
    "В консервации": true,
    "Не в резерве": true,
  });

  const [inputSelect, setInputSelect] = useState(
    getDataId(props.dataSchemeObjects)
  );

  let host = 'http://web.vst.vitebsk.energo.net/mnemo/api/'
  try {
    if (!process.env.NODE_ENV || process.env.NODE_ENV === 'development') {
      host = 'http://localhost:62902/'
    } else {
        // production code
    }
  } catch (e) {}

  function loadTableData() {
    const path = `${host}tags/GetTableData`;

    let formData = new FormData();

    fetch(path, {
      method: "POST",
      body: appendFormData(formData),
      credentials: "include",
    })
      .then((response) => response.json())
      .then((data) => {
        console.log(data);
        setDataArchive(data);
      });
  }

  function loadGraphData() {
    const path = `${host}tags/GetGraphData`;

    let formData = new FormData();

    fetch(path, {
      method: "POST",
      body: appendFormData(formData),
      credentials: "include",
    })
      .then((response) => response.json())
      .then((data) => setDataArchive(data));
  }

  function appendFormData(formData) {
    formData.append(
      "dateFrom",
      `${inputDates.dateFrom} ${inputDates.timeFrom}`
    );
    formData.append("dateTo", `${inputDates.dateTo} ${inputDates.timeTo}`);
    formData.append("withChanges", inputChanges["С изменениями"]);

    formData.append(
      "inputTypes",
      Array.from(Object.keys(inputTypes))
        .filter((e) => inputTypes[e])
        .join("|")
    );
    formData.append(
      "inputStates",
      Array.from(Object.keys(inputStates))
        .filter((e) => inputStates[e])
        .join("|")
    );
    formData.append("inputSelect", inputSelect.join("|"));

    return formData;
  }

  function getDataId(data) {
    let id = [];
    data.map((item) => id.push(item.Id));
    return id;
  }

  function showArchiveList() {
    if (!validDate()) {
      setIsShowArchiveList(true);
      setIsShowArchiveGraph(false);
      loadTableData();
    }
  }

  function showArchiveGraph() {
    if (!validDate()) {
      setIsShowArchiveList(false);
      setIsShowArchiveGraph(true);
      loadGraphData();
    }
  }

  function validDate() {
    const wrongDate = Object.values(inputDates).includes("");
    setIsWrongDate(wrongDate);

    return wrongDate;
  }

  function changeInputValue(inputState, newValue, inputCategory, functionSet) {
    const cloneInputsDateState = Object.assign({}, inputCategory);
    cloneInputsDateState[inputState] = newValue;
    functionSet(cloneInputsDateState);
  }

  return (
    <form>
      <Dialog
        fullScreen={true}
        open={props.showArchive}
        onClose={props.hiddenArchive}
      >
        <Paper className={styles.archive} square>
          <div className={styles.archiveForm}>
            <div className={styles.archiveBlock}>
              <Stack spacing={4}>
                <Button variant="contained" onClick={props.hiddenArchive}>
                  Выйти из архива
                </Button>
                <Stack spacing={1}>
                  <Button variant="outlined" onClick={showArchiveList}>
                    Отобразить списком
                  </Button>
                  <Button variant="outlined" onClick={showArchiveGraph}>
                    Отобразить графиком
                  </Button>
                </Stack>
              </Stack>
              <Stack spacing={2}>
                <div className={styles.archiveBlock}>
                  <TextField
                    id="dateFrom"
                    label="Начальная дата"
                    type="date"
                    sx={{ width: 150 }}
                    InputLabelProps={{
                      shrink: true,
                    }}
                    value={inputDates.dateFrom}
                    onChange={(e) =>
                      changeInputValue(
                        "dateFrom",
                        e.target.value,
                        inputDates,
                        setInputDates
                      )
                    }
                  />
                  <TextField
                    id="timeFrom"
                    label="Начальное время"
                    type="time"
                    sx={{ width: 150 }}
                    InputLabelProps={{
                      shrink: true,
                    }}
                    value={inputDates.timeFrom}
                    onChange={(e) =>
                      changeInputValue(
                        "timeFrom",
                        e.target.value,
                        inputDates,
                        setInputDates
                      )
                    }
                  />
                </div>
                <div className={styles.archiveBlock}>
                  <TextField
                    id="dateTo"
                    label="Конечная дата"
                    type="date"
                    sx={{ width: 150 }}
                    InputLabelProps={{
                      shrink: true,
                    }}
                    value={inputDates.dateTo}
                    onChange={(e) =>
                      changeInputValue(
                        "dateTo",
                        e.target.value,
                        inputDates,
                        setInputDates
                      )
                    }
                  />
                  <TextField
                    id="timeTo"
                    label="Конечное время"
                    type="time"
                    sx={{ width: 150 }}
                    InputLabelProps={{
                      shrink: true,
                    }}
                    value={inputDates.timeTo}
                    onChange={(e) =>
                      changeInputValue(
                        "timeTo",
                        e.target.value,
                        inputDates,
                        setInputDates
                      )
                    }
                  />
                </div>
                <div style={{ color: "red" }}>
                  {isWrongDate && "*Неверно указана дата"}
                </div>
                <FormControlLabel
                  control={<Checkbox />}
                  label="Показать только объекты с изменениями"
                  sx={{ "& .MuiSvgIcon-root": { fontSize: 18 } }}
                  checked={inputChanges["С изменениями"]}
                  onChange={(e) =>
                    changeInputValue(
                      "С изменениями",
                      e.target.checked,
                      inputChanges,
                      setInputChanges
                    )
                  }
                />
              </Stack>
            </div>
            <div style={{ display: "flex", gap: "100px" }}>
              <div className={styles.archiveTypes}>
                <Typography variant="body1">Типы:</Typography>
                <FormGroup>
                  <FormControlLabel
                    control={
                      <Checkbox
                        checked={inputTypes.Котел}
                        onChange={(e) =>
                          changeInputValue(
                            "Котел",
                            e.target.checked,
                            inputTypes,
                            setInputTypes
                          )
                        }
                      />
                    }
                    label="Котел"
                    sx={{ "& .MuiSvgIcon-root": { fontSize: 18 } }}
                  />
                  <FormControlLabel
                    control={
                      <Checkbox
                        checked={inputTypes.Насос}
                        onChange={(e) =>
                          changeInputValue(
                            "Насос",
                            e.target.checked,
                            inputTypes,
                            setInputTypes
                          )
                        }
                      />
                    }
                    label="Насос"
                    sx={{ "& .MuiSvgIcon-root": { fontSize: 18 } }}
                  />
                  <FormControlLabel
                    control={
                      <Checkbox
                        checked={inputTypes.Регулятор}
                        onChange={(e) =>
                          changeInputValue(
                            "Регулятор",
                            e.target.checked,
                            inputTypes,
                            setInputTypes
                          )
                        }
                      />
                    }
                    label="Регулятор"
                    sx={{ "& .MuiSvgIcon-root": { fontSize: 18 } }}
                  />
                  <FormControlLabel
                    control={
                      <Checkbox
                        checked={inputTypes.РОУ}
                        onChange={(e) =>
                          changeInputValue(
                            "РОУ",
                            e.target.checked,
                            inputTypes,
                            setInputTypes
                          )
                        }
                      />
                    }
                    label="РОУ"
                    sx={{ "& .MuiSvgIcon-root": { fontSize: 18 } }}
                  />
                  <FormControlLabel
                    control={
                      <Checkbox
                        checked={inputTypes.Турбина}
                        onChange={(e) =>
                          changeInputValue(
                            "Турбина",
                            e.target.checked,
                            inputTypes,
                            setInputTypes
                          )
                        }
                      />
                    }
                    label="Турбина"
                    sx={{ "& .MuiSvgIcon-root": { fontSize: 18 } }}
                  />
                </FormGroup>
              </div>
              <div className={styles.archiveTypes}>
                <Typography variant="body1">Состояния:</Typography>
                <FormGroup>
                  <FormControlLabel
                    control={<Checkbox />}
                    label="В горячем резерве"
                    sx={{ "& .MuiSvgIcon-root": { fontSize: 18 } }}
                    checked={inputStates["В горячем резерве"]}
                    onChange={(e) =>
                      changeInputValue(
                        "В горячем резерве",
                        e.target.checked,
                        inputStates,
                        setInputStates
                      )
                    }
                  />
                  <FormControlLabel
                    control={<Checkbox />}
                    label="В холодном резерве"
                    sx={{ "& .MuiSvgIcon-root": { fontSize: 18 } }}
                    checked={inputStates["В холодном резерве"]}
                    onChange={(e) =>
                      changeInputValue(
                        "В холодном резерве",
                        e.target.checked,
                        inputStates,
                        setInputStates
                      )
                    }
                  />
                  <FormControlLabel
                    control={<Checkbox />}
                    label="В капитальном ремонте"
                    sx={{ "& .MuiSvgIcon-root": { fontSize: 18 } }}
                    checked={inputStates["В капитальном ремонте"]}
                    onChange={(e) =>
                      changeInputValue(
                        "В капитальном ремонте",
                        e.target.checked,
                        inputStates,
                        setInputStates
                      )
                    }
                  />
                  <FormControlLabel
                    control={<Checkbox />}
                    label="В текущем ремонте"
                    sx={{ "& .MuiSvgIcon-root": { fontSize: 18 } }}
                    checked={inputStates["В текущем ремонте"]}
                    onChange={(e) =>
                      changeInputValue(
                        "В текущем ремонте",
                        e.target.checked,
                        inputStates,
                        setInputStates
                      )
                    }
                  />
                  <FormControlLabel
                    control={<Checkbox />}
                    label="В работе"
                    sx={{ "& .MuiSvgIcon-root": { fontSize: 18 } }}
                    checked={inputStates["В работе"]}
                    onChange={(e) =>
                      changeInputValue(
                        "В работе",
                        e.target.checked,
                        inputStates,
                        setInputStates
                      )
                    }
                  />
                  <FormControlLabel
                    control={<Checkbox />}
                    label="В консервации"
                    sx={{ "& .MuiSvgIcon-root": { fontSize: 18 } }}
                    checked={inputStates["В консервации"]}
                    onChange={(e) =>
                      changeInputValue(
                        "В консервации",
                        e.target.checked,
                        inputStates,
                        setInputStates
                      )
                    }
                  />
                  <FormControlLabel
                    control={<Checkbox />}
                    label="Не в резерве"
                    sx={{ "& .MuiSvgIcon-root": { fontSize: 18 } }}
                    checked={inputStates["Не в резерве"]}
                    onChange={(e) =>
                      changeInputValue(
                        "Не в резерве",
                        e.target.checked,
                        inputStates,
                        setInputStates
                      )
                    }
                  />
                </FormGroup>
              </div>
            </div>
            <div>
              <FormControl sx={{ m: 1, minWidth: 120, maxWidth: 500 }}>
                <InputLabel shrink htmlFor="select-multiple-id">
                  Объекты:
                </InputLabel>
                <Select
                  multiple
                  native
                  label="Объекты:"
                  inputProps={{
                    id: "select-multiple-id",
                  }}
                  value={inputSelect}
                  onChange={useCallback(
                    (event) => {
                      setInputSelect(
                        Array.from(
                          event.target.selectedOptions,
                          ({ value }) => value
                        )
                      );
                    },
                    [setInputSelect]
                  )}
                >
                  {getDataId(props.dataSchemeObjects).map((Id) => {
                    return (
                      <option key={Id} value={Id}>
                        {Id}
                      </option>
                    );
                  })}
                </Select>
              </FormControl>
            </div>
          </div>
          <div className={styles.archiveView}>
            {(isShowArchiveList && <ArchiveList dataTable={dataArchive} />) ||
              (isShowArchiveGraph && <ArchiveGraph dataGraph={dataArchive} />)}
          </div>
        </Paper>
      </Dialog>
    </form>
  );
}
export default Archive;
