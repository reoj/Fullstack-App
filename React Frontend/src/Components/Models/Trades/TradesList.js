import React, { Fragment, useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux';
import { fetchInitialState } from '../../../Context/Trades/trade-redux-actions';
import TradeSingle from './TradeSingle';
import TableDisplayer from "../../UI/TableDisplayer";

function TradesList() {
  const tradeList = useSelector((state) => state.root.trades.value.list);
  const dsp = useDispatch()

  useEffect(() => {
    dsp(fetchInitialState());
  }, [dsp])

  const properties = ["ID", "Sender", "Reciever","Item","Description", "Quantity"];
  return (
    <Fragment>
      <TableDisplayer modelType="Trades" colList={properties}>
        {tradeList.map((t) => {
          return (
            <TradeSingle 
            key={"Trade__" + t.id}
            id={t.id}
            sender={t.sender}
            Reciever={t.reciever}
            itemName={t.itemName}
            itemDescription={t.itemDescription}
            itemQuantity={t.itemQuantity}/>
          );
        })}
      </TableDisplayer>
    </Fragment>
  )
}

export default TradesList