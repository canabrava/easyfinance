import React, { useState, useRef, useEffect, useCallback } from 'react';
import { updatesplitBoardWidths, getSplitBoardWidths } from './splitBoardSignal';
import styles from './SplitBoard.module.css';

const SplitBoard = ({ id, leftChild, rightChild }) => {

  const [split, setSplit] = useState(getSplitBoardWidths(id));
  const splitBoardRef = useRef(null);

  useEffect(() => {
    if (getSplitBoardWidths(id) !== split) {
      updatesplitBoardWidths(id, split);
    }
  }, [split]);

  const handleMouseMove = useCallback((event) => {
    event.preventDefault();
    
    if (!splitBoardRef.current) {
        return;
      }
  
      const rect = splitBoardRef.current.getBoundingClientRect();
      const x = event.clientX - rect.left;
      const widthPercentage = (x / rect.width) * 100;

    let newSplit;
    if(widthPercentage < 0){
        newSplit = 0;
    }
    else if(widthPercentage > 100){
        newSplit = 100;
    }
    else {
        newSplit = widthPercentage;
    }

    if (split !== newSplit) {
        setSplit(newSplit);
    }

  }, [split]);

  const handleButtonClick = useCallback((direction) => {
    setSplit((prevSplit) => {
      if (direction === 'left') {
        return 0;
      } else if (direction === 'right') {
        return 100;
      } else {
        return prevSplit;
      }
    });
  }, []);

  const handleMouseUp = () => {
    window.removeEventListener('mousemove', handleMouseMove);
    window.removeEventListener('mouseup', handleMouseUp);
  };

  const handleMouseDown = () => {
    window.addEventListener('mousemove', handleMouseMove);
    window.addEventListener('mouseup', handleMouseUp);
  };

  return (
  <div ref={splitBoardRef} className={styles.splitBoard}>
    <div className={styles.leftPane} style={{ width: `${split}%` }}>
      {leftChild}
    </div>
    <div className={split === 0 || split === 100 ? styles.dividerContainerHalf : styles.dividerContainer} onMouseDown={handleMouseDown}>
      {split > 0 && (
        <div>
          <svg className={styles.button} onClick={() => handleButtonClick('left')} xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M15 19l-7-7 7-7" />
          </svg>
        </div>
      )}
      {split < 100 && (
        <div>
          <svg className={styles.button} onClick={() => handleButtonClick('right')} xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M9 5l7 7-7 7" />
          </svg>
        </div>
      )}
  </div>
    <div className={styles.rightPane} style={{ width: `${100 - split}%` }}>
      {rightChild}
    </div>
  </div>
);
};

export default SplitBoard;