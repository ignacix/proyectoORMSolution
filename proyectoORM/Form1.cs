using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proyectoORM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            refrescarGrilla();

            groupBox2.Visible = false;
            groupBox3.Visible = false;
        }

        private void btninsertar_Click(object sender, EventArgs e)
        {
            DISCOS disco = new DISCOS();

            disco.Titulo = txtNombre.Text;
            disco.CantidadCanciones = int.Parse(txtCantCanciones.Text);

            using (DISCOS_DBEntities db = new DISCOS_DBEntities())
            {
                db.DISCOS.Add(disco);
                db.SaveChanges();
                refrescarGrilla();
            }


        }


        private void refrescarGrilla()
        {
            using (DISCOS_DBEntities db = new DISCOS_DBEntities())
            {
                var tabla = db.DISCOS;
                List<DISCOS> lista = new List<DISCOS>();

                foreach (DISCOS discos in tabla)
                {
                    lista.Add(discos);
                }

                dataGridView1.DataSource = lista;

            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            using (DISCOS_DBEntities db = new DISCOS_DBEntities())
            {
                DISCOS disco = db.DISCOS.Find(int.Parse(txtId.Text));                
                disco.Titulo = txtNombreModificar.Text;

                db.Entry(disco).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                refrescarGrilla();
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            DISCOS discActual = dataGridView1.CurrentRow.DataBoundItem as DISCOS;
            
            if (groupBox2.Visible==true)
            {
                txtId.Text = discActual.Id.ToString();
                txtNombreModificar.Text = discActual.Titulo.ToString();
            }
            else if (groupBox3.Visible == true)
            {
                txtIdEliminar.Text = discActual.Id.ToString();                
            }


        }

        private void btnSeleccion_Click(object sender, EventArgs e)
        {
            HacerVsible(groupBox1);                        
            groupBox2.Visible = false;
            groupBox3.Visible = false;

        }

        private void HacerVsible(GroupBox gbx)
        {
            gbx.Visible = true;
            Point point = new Point(12, 206);
            gbx.Location = point;
        }

        private void btnOpcionModificar_Click(object sender, EventArgs e)
        {
            HacerVsible(groupBox2);
            groupBox1.Visible = false;
            groupBox3.Visible=false;
        }

        private void btnOpcionEliminar_Click(object sender, EventArgs e)
        {
            HacerVsible(groupBox3);
            groupBox1.Visible = false;
            groupBox2.Visible = false;

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            using (DISCOS_DBEntities db = new DISCOS_DBEntities())
            {
                DISCOS disco = db.DISCOS.Find(int.Parse(txtIdEliminar.Text));

                DialogResult resultado = MessageBox.Show("Seguro que quiere Eliminarlo", "camption", MessageBoxButtons.YesNo);
                if(resultado==DialogResult.Yes)
                {
                    db.DISCOS.Remove(disco);
                    db.Entry(disco).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                    refrescarGrilla();

                }
            }
        }
    }
}
