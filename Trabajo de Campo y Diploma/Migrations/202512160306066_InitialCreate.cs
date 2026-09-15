namespace Trabajo_de_Campo_y_Diploma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        id_cliente = c.Int(nullable: false, identity: true),
                        dni = c.Int(),
                        nombre = c.String(maxLength: 60),
                        email = c.String(maxLength: 60),
                        telefono = c.String(maxLength: 20),
                        estado = c.Boolean(),
                    })
                .PrimaryKey(t => t.id_cliente);
            
            CreateTable(
                "dbo.Formularios",
                c => new
                    {
                        id_formulario = c.Int(nullable: false, identity: true),
                        nombre = c.String(maxLength: 30),
                        id_modulo = c.Int(),
                    })
                .PrimaryKey(t => t.id_formulario)
                .ForeignKey("dbo.Modulos", t => t.id_modulo)
                .Index(t => t.id_modulo);
            
            CreateTable(
                "dbo.Modulos",
                c => new
                    {
                        id_modulo = c.Int(nullable: false, identity: true),
                        nombre = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.id_modulo);
            
            CreateTable(
                "dbo.Permisos",
                c => new
                    {
                        id_permiso = c.Int(nullable: false, identity: true),
                        nombre_permiso = c.String(maxLength: 50),
                        id_formulario = c.Int(),
                        estado = c.Boolean(),
                    })
                .PrimaryKey(t => t.id_permiso)
                .ForeignKey("dbo.Formularios", t => t.id_formulario)
                .Index(t => t.id_formulario);
            
            CreateTable(
                "dbo.Grupos",
                c => new
                    {
                        id_grupo = c.Int(nullable: false, identity: true),
                        grupo_nombre = c.String(maxLength: 60),
                        estado = c.Boolean(),
                    })
                .PrimaryKey(t => t.id_grupo);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        id_usuario = c.Int(nullable: false, identity: true),
                        nombre = c.String(maxLength: 60),
                        usuario = c.String(maxLength: 60),
                        dni = c.String(maxLength: 15),
                        apellido = c.String(maxLength: 60),
                        email = c.String(maxLength: 60),
                        clave = c.String(maxLength: 130),
                        estado = c.Boolean(),
                    })
                .PrimaryKey(t => t.id_usuario);
            
            CreateTable(
                "dbo.Linea_Pedidos",
                c => new
                    {
                        LineaPedidoId = c.Int(nullable: false, identity: true),
                        PedidoId = c.Int(nullable: false),
                        ProductoId = c.Int(nullable: false),
                        Cantidad = c.Decimal(nullable: false, precision: 18, scale: 2, storeType: "numeric"),
                        PrecioUnitario = c.Decimal(nullable: false, precision: 18, scale: 2, storeType: "numeric"),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2, storeType: "numeric"),
                    })
                .PrimaryKey(t => t.LineaPedidoId)
                .ForeignKey("dbo.Pedidos", t => t.PedidoId, cascadeDelete: true)
                .ForeignKey("dbo.Productos", t => t.ProductoId, cascadeDelete: true)
                .Index(t => t.PedidoId)
                .Index(t => t.ProductoId);
            
            CreateTable(
                "dbo.Pedidos",
                c => new
                    {
                        PedidoId = c.Int(nullable: false, identity: true),
                        id_cliente = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        Observaciones = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.PedidoId)
                .ForeignKey("dbo.Clientes", t => t.id_cliente, cascadeDelete: true)
                .Index(t => t.id_cliente);
            
            CreateTable(
                "dbo.Productos",
                c => new
                    {
                        ProductoId = c.Int(nullable: false, identity: true),
                        descripcion = c.String(maxLength: 60),
                        stock = c.Int(),
                        costo_producto = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ProductoId);
            
            CreateTable(
                "dbo.Linea_Presupuestos",
                c => new
                    {
                        LineaPresupuestoId = c.Int(nullable: false, identity: true),
                        PresupuestoId = c.Int(nullable: false),
                        ProductoId = c.Int(nullable: false),
                        Cantidad = c.Decimal(nullable: false, precision: 18, scale: 2, storeType: "numeric"),
                        PrecioUnitario = c.Decimal(nullable: false, precision: 18, scale: 2, storeType: "numeric"),
                        PrecioTotal = c.Decimal(nullable: false, precision: 18, scale: 2, storeType: "numeric"),
                    })
                .PrimaryKey(t => t.LineaPresupuestoId)
                .ForeignKey("dbo.Presupuestos", t => t.PresupuestoId, cascadeDelete: true)
                .ForeignKey("dbo.Productos", t => t.ProductoId, cascadeDelete: true)
                .Index(t => t.PresupuestoId)
                .Index(t => t.ProductoId);
            
            CreateTable(
                "dbo.Presupuestos",
                c => new
                    {
                        PresupuestoId = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        ClienteId = c.Int(nullable: false),
                        UsuarioId = c.Int(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2, storeType: "numeric"),
                    })
                .PrimaryKey(t => t.PresupuestoId)
                .ForeignKey("dbo.Clientes", t => t.ClienteId, cascadeDelete: true)
                .Index(t => t.ClienteId);
            
            CreateTable(
                "dbo.GruposPermisos",
                c => new
                    {
                        Grupos_id_grupo = c.Int(nullable: false),
                        Permisos_id_permiso = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Grupos_id_grupo, t.Permisos_id_permiso })
                .ForeignKey("dbo.Grupos", t => t.Grupos_id_grupo, cascadeDelete: true)
                .ForeignKey("dbo.Permisos", t => t.Permisos_id_permiso, cascadeDelete: true)
                .Index(t => t.Grupos_id_grupo)
                .Index(t => t.Permisos_id_permiso);
            
            CreateTable(
                "dbo.UsuariosGrupos",
                c => new
                    {
                        Usuarios_id_usuario = c.Int(nullable: false),
                        Grupos_id_grupo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Usuarios_id_usuario, t.Grupos_id_grupo })
                .ForeignKey("dbo.Usuarios", t => t.Usuarios_id_usuario, cascadeDelete: true)
                .ForeignKey("dbo.Grupos", t => t.Grupos_id_grupo, cascadeDelete: true)
                .Index(t => t.Usuarios_id_usuario)
                .Index(t => t.Grupos_id_grupo);
            
            CreateTable(
                "dbo.UsuariosPermisos",
                c => new
                    {
                        Usuarios_id_usuario = c.Int(nullable: false),
                        Permisos_id_permiso = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Usuarios_id_usuario, t.Permisos_id_permiso })
                .ForeignKey("dbo.Usuarios", t => t.Usuarios_id_usuario, cascadeDelete: true)
                .ForeignKey("dbo.Permisos", t => t.Permisos_id_permiso, cascadeDelete: true)
                .Index(t => t.Usuarios_id_usuario)
                .Index(t => t.Permisos_id_permiso);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Linea_Presupuestos", "ProductoId", "dbo.Productos");
            DropForeignKey("dbo.Linea_Presupuestos", "PresupuestoId", "dbo.Presupuestos");
            DropForeignKey("dbo.Presupuestos", "ClienteId", "dbo.Clientes");
            DropForeignKey("dbo.Linea_Pedidos", "ProductoId", "dbo.Productos");
            DropForeignKey("dbo.Linea_Pedidos", "PedidoId", "dbo.Pedidos");
            DropForeignKey("dbo.Pedidos", "id_cliente", "dbo.Clientes");
            DropForeignKey("dbo.UsuariosPermisos", "Permisos_id_permiso", "dbo.Permisos");
            DropForeignKey("dbo.UsuariosPermisos", "Usuarios_id_usuario", "dbo.Usuarios");
            DropForeignKey("dbo.UsuariosGrupos", "Grupos_id_grupo", "dbo.Grupos");
            DropForeignKey("dbo.UsuariosGrupos", "Usuarios_id_usuario", "dbo.Usuarios");
            DropForeignKey("dbo.GruposPermisos", "Permisos_id_permiso", "dbo.Permisos");
            DropForeignKey("dbo.GruposPermisos", "Grupos_id_grupo", "dbo.Grupos");
            DropForeignKey("dbo.Permisos", "id_formulario", "dbo.Formularios");
            DropForeignKey("dbo.Formularios", "id_modulo", "dbo.Modulos");
            DropIndex("dbo.UsuariosPermisos", new[] { "Permisos_id_permiso" });
            DropIndex("dbo.UsuariosPermisos", new[] { "Usuarios_id_usuario" });
            DropIndex("dbo.UsuariosGrupos", new[] { "Grupos_id_grupo" });
            DropIndex("dbo.UsuariosGrupos", new[] { "Usuarios_id_usuario" });
            DropIndex("dbo.GruposPermisos", new[] { "Permisos_id_permiso" });
            DropIndex("dbo.GruposPermisos", new[] { "Grupos_id_grupo" });
            DropIndex("dbo.Presupuestos", new[] { "ClienteId" });
            DropIndex("dbo.Linea_Presupuestos", new[] { "ProductoId" });
            DropIndex("dbo.Linea_Presupuestos", new[] { "PresupuestoId" });
            DropIndex("dbo.Pedidos", new[] { "id_cliente" });
            DropIndex("dbo.Linea_Pedidos", new[] { "ProductoId" });
            DropIndex("dbo.Linea_Pedidos", new[] { "PedidoId" });
            DropIndex("dbo.Permisos", new[] { "id_formulario" });
            DropIndex("dbo.Formularios", new[] { "id_modulo" });
            DropTable("dbo.UsuariosPermisos");
            DropTable("dbo.UsuariosGrupos");
            DropTable("dbo.GruposPermisos");
            DropTable("dbo.Presupuestos");
            DropTable("dbo.Linea_Presupuestos");
            DropTable("dbo.Productos");
            DropTable("dbo.Pedidos");
            DropTable("dbo.Linea_Pedidos");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Grupos");
            DropTable("dbo.Permisos");
            DropTable("dbo.Modulos");
            DropTable("dbo.Formularios");
            DropTable("dbo.Clientes");
        }
    }
}
