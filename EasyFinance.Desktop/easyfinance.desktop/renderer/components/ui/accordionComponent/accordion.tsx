import { useState } from 'react';

const Accordion = ({ title, children }) => {
  const [isOpen, setIsOpen] = useState(false);

  return (
    <div className="overflow-hidden border rounded">
      <div 
        className="flex items-center justify-between px-3 py-2 font-bold transition-colors duration-200 cursor-pointer bg-tertiary text-primary hover:text-darktertiary"  
        onClick={() => setIsOpen(!isOpen)}
      >
        <span className=''>{title}</span>
        <svg className={`w-5 h-5 transform transition-transform duration-200 ${isOpen ? 'rotate-180' : ''}`} fill="currentColor" viewBox="0 0 20 20">
          <path fillRule="evenodd" d="M5.293 7.293a1 1 0 011.414 0L10 10.586l3.293-3.293a1 1 0 111.414 1.414l-4 4a1 1 0 01-1.414 0l-4-4a1 1 0 010-1.414z" clipRule="evenodd"></path>
        </svg>
      </div>
      {isOpen && <div className="px-3 py-2">{children}</div>}
    </div>
  );
};

export default Accordion;