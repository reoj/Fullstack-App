import React, { Fragment } from "react";
import { useSelector } from "react-redux";
import Card from "react-bootstrap/Card";
import Spinner from "react-bootstrap/Spinner"

function StatusCard() {
  const errorState = useSelector((state) => state.root.status.value.error);
  const loadingState = useSelector((state) => state.root.status.value.loading);
  return (
    <Fragment>
      <div className="row w-100 justify-content-center mt-5">
        {errorState !== "" && (
          <Card className="w-25 position-relative p-3 text-danger ">
            {errorState}
          </Card>
        )}
        {(loadingState !== false) && (
          <Card className="w-25 p-3 text-black">
            <div className="d-flex justify-content-center">
            <Spinner animation="border" role="status" aria-hidden="true"/>
            </div>
            <span className="mt-3">Loading...</span>
            
          </Card>
        )}
      </div>
    </Fragment>
  );
}

export default StatusCard;
