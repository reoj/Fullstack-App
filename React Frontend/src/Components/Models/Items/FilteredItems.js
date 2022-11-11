import React, { Fragment, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import {useParams} from "react-router-dom"
import { fetchInitialState } from "../../../Context/Items/items-redux-actions";
import TableDisplayer from "../../UI/TableDisplayer";
import ItemSingle from "./ItemSingle";

function FilteredItems(props) {
  const fullList = useSelector((state) => state.root.item.value.list);
  const userOnDisplay = useParams();

  const dsp = useDispatch()
  useEffect(() => {
    dsp(fetchInitialState());
  }, [dsp])

  const itemsList = fullList.filter(i=> i.userId === +userOnDisplay.userId)

  const properties = ["ID", "Name", "Description", "Quantity","Owner ID"];

  return (
    <Fragment>
    <TableDisplayer
      modelType="Filtered Items"
      colList={properties}
    >
      {itemsList.map((i) => {
        return (
          <ItemSingle
            key={"Item" + i.id}
            idn={i.id}
            name={i.name}
            description={i.description}
            quantity={i.quantity}
            owner={i.userId}
          />
        );
      })}
    </TableDisplayer>
  </Fragment>
  );
}

export default FilteredItems;
