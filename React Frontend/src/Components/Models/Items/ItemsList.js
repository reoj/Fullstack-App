import React, { useEffect, Fragment} from "react";
import { useSelector } from "react-redux";
import TableDisplayer from "../../UI/TableDisplayer";
import ItemSingle from "./ItemSingle";
import { fetchInitialState } from "../../../Context/Items/items-redux-actions";
import { useDispatch } from "react-redux";

function ItemsList(props) {
  const itemsList = useSelector((state) => state.root.item.value.list);

  const dsp = useDispatch()

  useEffect(() => {
    dsp(fetchInitialState());
  }, [])
  

  const properties = ["ID", "Name", "Description", "Quantity","Owner ID"];

  return (
    <Fragment>
    <TableDisplayer
      modelType="Items"
      colList={properties}
    >
      {itemsList.map((i) => {
        return (
          <ItemSingle
            key={"Item" + i.id.toString()}
            idn={i.id}
            name = {i.name}
            description={i.description}
            owner={i.userId}
            quantity={i.quantity}
          />
        );
      })}
    </TableDisplayer>
  </Fragment>
  );
}

export default ItemsList;
