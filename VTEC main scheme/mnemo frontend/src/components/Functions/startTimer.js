import { useState, useEffect } from 'react';

export function useStartTimer(funcForLoad, pathForLoad, funcForUpdate, pathForUpdate, time, needUpdate = 0) {
    const [timer, setTimer] = useState(0);

    useEffect(() => {
        fetch(pathForLoad, { credentials: "include" })
          .then((response) => response.json())
          .then((data) => {
            funcForLoad(data)
            setTimer(timer + 1);
          });
      }, [pathForLoad, needUpdate]);

      useEffect(() => {
        let interval = null;
    
        if (timer !== 0) {
          function startStuff(func, time) {
            interval = setInterval(func, time);
          }
    
          startStuff(() => {    
            fetch(pathForUpdate, { credentials: "include" })
              .then((response) => response.json())
              .then((data) => {
                funcForUpdate(data)
                setTimer(timer + 1);
              });
          }, time);
        }
    
        return () => clearInterval(interval);
      }, [timer, pathForUpdate, needUpdate]);
  };
