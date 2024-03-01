import React from 'react';

const AddButton = ({ onClick }) => {
  return (
    <button
      onClick={onClick}
      className="btn-primary-color py-2 px-4 rounded-full flex items-center justify-center w-12 h-12"
      aria-label="Add"
    >
      <svg
        className="w-6 h-6"
        fill="none"
        stroke="currentColor"
        viewBox="0 0 24 24"
        xmlns="http://www.w3.org/2000/svg"
      >
        <path
          strokeLinecap="round"
          strokeLinejoin="round"
          strokeWidth="4"
          d="M12 4v16m8-8H4"
        ></path>
      </svg>
    </button>
  );
};

export default AddButton;
