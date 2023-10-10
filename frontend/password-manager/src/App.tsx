import { useEffect, useState } from "react";
import { DataGrid, GridColDef } from "@mui/x-data-grid";
import Typography from "@mui/material/Typography";
import Toolbar from "@mui/material/Toolbar";
import AppBar from "@mui/material/AppBar";
import "./App.css";
import { Button } from "@mui/material";
import Box from "@mui/material/Box";
import axios from "axios";
import NewPasswordModal from "./NewPasswordModal";

const passwordsUrl = "http://localhost:5000/passwords";

const columns: GridColDef[] = [
  { field: "id", headerName: "ID", width: 70 },
  { field: "name", headerName: "Name", width: 500 },
  {
    field: "copy",
    headerName: "Copy",

    renderCell: (params) => (
      <Button onClick={() => copyPasswordToClipboard(params.row.id)}>
        Copy
      </Button>
    ),
  },
];

function copyPasswordToClipboard(id: string) {
  axios
    .get(`${passwordsUrl}/${id}`)
    .then((res) => navigator.clipboard.writeText(res.data.value));
}

interface Password {
  name: string;
  value: string;
}

function App() {
  const [passwords, setPasswords] = useState<Password[]>([]);
  const [open, setOpen] = useState(false);

  function loadPasswords() {
    axios.get(passwordsUrl).then((res) => {
      console.log(res.data), setPasswords(res.data);
    });
  }

  useEffect(() => {
    loadPasswords();
  }, []);

  return (
    <>
      <Box sx={{ flexGrow: 1 }}>
        <AppBar position="static" style={{ paddingLeft: 10 }}>
          <Toolbar>
            <Typography variant="h4" component="div" sx={{ flexGrow: 1 }}>
              Password Manager
            </Typography>
            <Button color="inherit" onClick={() => setOpen(true)}>
              Add
            </Button>
          </Toolbar>
        </AppBar>
      </Box>
      <div style={{ height: "800px", width: "100%" }}>
        <DataGrid
          rows={passwords}
          columns={columns}
          initialState={{
            pagination: {
              paginationModel: { page: 0, pageSize: 5 },
            },
          }}
          pageSizeOptions={[5, 10]}
        />
      </div>
      <NewPasswordModal
        open={open}
        setOpen={setOpen}
        reloadPasswords={() => loadPasswords()}
      />
    </>
  );
}

export default App;
