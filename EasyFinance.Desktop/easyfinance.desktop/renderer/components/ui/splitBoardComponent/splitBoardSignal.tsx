import { signal } from "@preact/signals-react"

const splitBoardWidths: { [key: string]: number } = {};

const splitBoardWidthsSignal = signal(splitBoardWidths);

export function updatesplitBoardWidths(id: string, width: number){
    splitBoardWidthsSignal.value[id] = width;
}

export function getSplitBoardWidths(id: string) : number {
    const width = splitBoardWidthsSignal.value[id] !== undefined ? splitBoardWidthsSignal.value[id] : 50;
    return width;
}