-- Generado por Oracle SQL Developer Data Modeler 20.2.0.167.1538
--   en:        2020-11-01 01:20:02 CLST
--   sitio:      Oracle Database 12cR2
--   tipo:      Oracle Database 12cR2



DROP TABLE alertastock CASCADE CONSTRAINTS;

DROP TABLE bancomovimiento CASCADE CONSTRAINTS;

DROP TABLE bodegamovimiento CASCADE CONSTRAINTS;

DROP TABLE cajaestado CASCADE CONSTRAINTS;

DROP TABLE cajaoperacion CASCADE CONSTRAINTS;

DROP TABLE cliente CASCADE CONSTRAINTS;

DROP TABLE colacocina CASCADE CONSTRAINTS;

DROP TABLE detalleorden CASCADE CONSTRAINTS;

DROP TABLE detpedidoins CASCADE CONSTRAINTS;

DROP TABLE doctpagotipo CASCADE CONSTRAINTS;

DROP TABLE documentopago CASCADE CONSTRAINTS;

DROP TABLE egreso CASCADE CONSTRAINTS;

DROP TABLE empleadoturno CASCADE CONSTRAINTS;

DROP TABLE entidadbancaria CASCADE CONSTRAINTS;

DROP TABLE estadomesa CASCADE CONSTRAINTS;

DROP TABLE estadoorden CASCADE CONSTRAINTS;

DROP TABLE estadoproducto CASCADE CONSTRAINTS;

DROP TABLE estadoproveedor CASCADE CONSTRAINTS;

DROP TABLE estadoreserva CASCADE CONSTRAINTS;

DROP TABLE funcionalidad CASCADE CONSTRAINTS;

DROP TABLE ingrediente CASCADE CONSTRAINTS;

DROP TABLE ingreso CASCADE CONSTRAINTS;

DROP TABLE insumo CASCADE CONSTRAINTS;

DROP TABLE insumostock CASCADE CONSTRAINTS;

DROP TABLE mediopago CASCADE CONSTRAINTS;

DROP TABLE mediopagotx CASCADE CONSTRAINTS;

DROP TABLE mesa CASCADE CONSTRAINTS;

DROP TABLE orden CASCADE CONSTRAINTS;

DROP TABLE pagopedido CASCADE CONSTRAINTS;

DROP TABLE pedido CASCADE CONSTRAINTS;

DROP TABLE permisos CASCADE CONSTRAINTS;

DROP TABLE persona CASCADE CONSTRAINTS;

DROP TABLE prodpreparacion CASCADE CONSTRAINTS;

DROP TABLE producto CASCADE CONSTRAINTS;

DROP TABLE proveedor CASCADE CONSTRAINTS;

DROP TABLE recetaproducto CASCADE CONSTRAINTS;

DROP TABLE reserva CASCADE CONSTRAINTS;

DROP TABLE restaurant CASCADE CONSTRAINTS;

DROP TABLE rol CASCADE CONSTRAINTS;

DROP TABLE tipocuenta CASCADE CONSTRAINTS;

DROP TABLE tipomovbanco CASCADE CONSTRAINTS;

DROP TABLE tipomovbodega CASCADE CONSTRAINTS;

DROP TABLE tipopreparacion CASCADE CONSTRAINTS;

DROP TABLE tipoproducto CASCADE CONSTRAINTS;

DROP TABLE usuario CASCADE CONSTRAINTS;

DROP TABLE usuariorol CASCADE CONSTRAINTS;

-- predefined type, no DDL - MDSYS.SDO_GEOMETRY

-- predefined type, no DDL - XMLTYPE

CREATE TABLE alertastock (
    idalerta    NUMBER(4)
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    descalerta  VARCHAR2(150)
)
LOGGING;

ALTER TABLE alertastock ADD CONSTRAINT alertastock_pk PRIMARY KEY ( idalerta );

CREATE TABLE bancomovimiento (
    idbcomov       NUMBER(10)
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    montomov       NUMBER(10),
    numcuenta      NUMBER(10),
    idtipomovbco   NUMBER(2),
    identbancaria  NUMBER(2)
)
LOGGING;

ALTER TABLE bancomovimiento ADD CONSTRAINT bancomovimiento_pk PRIMARY KEY ( idbcomov );

CREATE TABLE bodegamovimiento (
    idmovbodega      NUMBER(10)
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    fechamovimiento  DATE,
    valorproducto    INTEGER,
    idtipomovbdga    NUMBER(4) NOT NULL,
    peso             NUMBER(6),
    idinsumo         NUMBER(10) NOT NULL
)
LOGGING;

ALTER TABLE bodegamovimiento ADD CONSTRAINT bodegamovimiento_pk PRIMARY KEY ( idmovbodega );

CREATE TABLE cajaestado (
    idcajaestado    NUMBER(2)
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    desccajaestado  VARCHAR2(30)
)
LOGGING;

ALTER TABLE cajaestado ADD CONSTRAINT cajaestado_pk PRIMARY KEY ( idcajaestado );

CREATE TABLE cajaoperacion (
    idcajaoperacion  NUMBER(6)
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    montooperacion   NUMBER(10),
    fechainiciocaja  DATE,
    fechacierrecaja  DATE,
    idcajaestado     NUMBER(2) NOT NULL,
    idpersona        NUMBER(10) NOT NULL,
    observacion      VARCHAR2(300)
)
LOGGING;

ALTER TABLE cajaoperacion ADD CONSTRAINT cajaoperacion_pk PRIMARY KEY ( idcajaoperacion );

CREATE TABLE cliente (
    idpersona NUMBER(10) NOT NULL
)
LOGGING;

COMMENT ON COLUMN cliente.idpersona IS
    'Auto incrementable';

ALTER TABLE cliente ADD CONSTRAINT cliente_pk PRIMARY KEY ( idpersona );

CREATE TABLE colacocina (
    idcolacocina    NUMBER(6)
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    horacreacion    DATE,
    horaactual      DATE,
    horaentrega     DATE,
    nropedido       NUMBER(6),
    estadopedido    NUMBER(2),
    cantidad        NUMBER(6),
    dificultadprep  NUMBER(4),
    tiempoprep      NUMBER(4)
)
LOGGING;

ALTER TABLE colacocina ADD CONSTRAINT colacocina_pk PRIMARY KEY ( idcolacocina );

CREATE TABLE detalleorden (
    iddetalleorden  NUMBER(6)
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    cantidad        NUMBER(4),
    idorden         NUMBER(10) NOT NULL,
    idproducto      NUMBER(6) NOT NULL,
    precioprod      NUMBER(6),
    idestado        NUMBER(2) NOT NULL,
    horadetord      DATE
)
LOGGING;

ALTER TABLE detalleorden ADD CONSTRAINT detalleorden_pk PRIMARY KEY ( iddetalleorden );

CREATE TABLE detpedidoins (
    idpedidoinsumo  NUMBER(4)
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    cantinsumo      NUMBER(10),
    fechapedido     DATE,
    idpedido        NUMBER(10) NOT NULL,
    idinsumo        NUMBER(10) NOT NULL
)
LOGGING;

ALTER TABLE detpedidoins ADD CONSTRAINT detpedidoins_pk PRIMARY KEY ( idpedidoinsumo );

CREATE TABLE doctpagotipo (
    iddoctpagotipo    NUMBER(2)
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    descdoctpagotipo  VARCHAR2(30) NOT NULL
)
LOGGING;

ALTER TABLE doctpagotipo ADD CONSTRAINT doctpagotipo_pk PRIMARY KEY ( iddoctpagotipo );

CREATE TABLE documentopago (
    iddoctopago     NUMBER(2)
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    idorden         INTEGER,
    total           INTEGER,
    idpersona       NUMBER(10),
    iddoctpagotipo  NUMBER(2),
    idmediopago     NUMBER(2),
    propina         NUMBER(10)
)
LOGGING;

ALTER TABLE documentopago ADD CONSTRAINT documentopago_pk PRIMARY KEY ( iddoctopago );

CREATE TABLE egreso (
    idegreso         NUMBER(10)
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    monto            NUMBER(10),
    descmovimiento   VARCHAR2(100),
    fechamovimiento  DATE
)
LOGGING;

CREATE UNIQUE INDEX egreso__idx ON
    egreso (
        idegreso
    ASC )
        LOGGING;

ALTER TABLE egreso ADD CONSTRAINT egreso_pk PRIMARY KEY ( idegreso );

CREATE TABLE empleadoturno (
    idempturno  NUMBER(4)
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    fechaturno  DATE,
    horadesde   INTEGER,
    horahasta   INTEGER,
    idpersona   NUMBER(10) NOT NULL
)
LOGGING;

ALTER TABLE empleadoturno ADD CONSTRAINT empleadoturno_pk PRIMARY KEY ( idempturno );

CREATE TABLE entidadbancaria (
    identbancaria  NUMBER(2)
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    descbanco      VARCHAR2(50),
    idtipocuenta   NUMBER(2) NOT NULL
)
LOGGING;

ALTER TABLE entidadbancaria ADD CONSTRAINT entidadbancaria_pk PRIMARY KEY ( identbancaria );

CREATE TABLE estadomesa (
    idestadomesa    NUMBER(2)
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    descestadomesa  VARCHAR2(50)
)
LOGGING;

ALTER TABLE estadomesa ADD CONSTRAINT estadomesa_pk PRIMARY KEY ( idestadomesa );

CREATE TABLE estadoorden (
    idestado      NUMBER(2)
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    descestorden  VARCHAR2(30)
)
LOGGING;

ALTER TABLE estadoorden ADD CONSTRAINT estadoorden_pk PRIMARY KEY ( idestado );

CREATE TABLE estadoproducto (
    idestproducto    NUMBER(2)
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    descestproducto  VARCHAR2(50)
)
LOGGING;

ALTER TABLE estadoproducto ADD CONSTRAINT estadoproducto_pk PRIMARY KEY ( idestproducto );

CREATE TABLE estadoproveedor (
    idestproveedor  NUMBER(2)
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    descestpro      VARCHAR2(30)
)
LOGGING;

ALTER TABLE estadoproveedor ADD CONSTRAINT estadoproveedor_pk PRIMARY KEY ( idestproveedor );

CREATE TABLE estadoreserva (
    idestadoreserva  NUMBER(2)
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    descestreserva   VARCHAR2(30)
)
LOGGING;

ALTER TABLE estadoreserva ADD CONSTRAINT estadoreserva_pk PRIMARY KEY ( idestadoreserva );

CREATE TABLE funcionalidad (
    idfuncionalidad  NUMBER(4)
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    descripcion      VARCHAR2(250),
    rutauri          VARCHAR2(100)
)
LOGGING;

ALTER TABLE funcionalidad ADD CONSTRAINT funcionalidad_pk PRIMARY KEY ( idfuncionalidad );

CREATE TABLE ingrediente (
    idingrediente                 NUMBER(10)
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    descingrediente               NVARCHAR2(250),
    caducidad                     NUMBER(4),
    stock                         NUMBER(4),
    medida                        NVARCHAR2(50),
    porcmerma                     NUMBER(4, 3),
    prodpreparacion_idprodprepar  NUMBER(6) NOT NULL
)
LOGGING;

ALTER TABLE ingrediente ADD CONSTRAINT bdgcocina_pk PRIMARY KEY ( idingrediente );

CREATE TABLE ingreso (
    idingreso        NUMBER(10)
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    monto            NUMBER(10),
    descingreso      VARCHAR2(250),
    fechamovimiento  DATE
)
LOGGING;

CREATE UNIQUE INDEX ingreso__idx ON
    ingreso (
        idingreso
    ASC )
        LOGGING;

ALTER TABLE ingreso ADD CONSTRAINT ingreso_pk PRIMARY KEY ( idingreso );

CREATE TABLE insumo (
    idinsumo      NUMBER(10)
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    descinsumo    VARCHAR2(100),
    precioinsumo  NUMBER(6) NOT NULL
)
LOGGING;

ALTER TABLE insumo ADD CONSTRAINT insumo_pk PRIMARY KEY ( idinsumo );

CREATE TABLE insumostock (
    idinsumostock  NUMBER(10)
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    stockactual    NUMBER(4),
    stockminimo    NUMBER(4),
    idinsumo       NUMBER(10) NOT NULL,
    peso           NUMBER(6)
)
LOGGING;

ALTER TABLE insumostock ADD CONSTRAINT insumostock_pk PRIMARY KEY ( idinsumostock );

CREATE TABLE mediopago (
    idmediopago    NUMBER(2)
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    descmediopago  VARCHAR2(30)
)
LOGGING;

ALTER TABLE mediopago ADD CONSTRAINT mediopago_pk PRIMARY KEY ( idmediopago );

CREATE TABLE mediopagotx (
    idmediopagotx  NUMBER(2)
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    rut            NUMBER(8),
    dvclientetx    VARCHAR2(1),
    descmediopago  VARCHAR2(30),
    monto          INTEGER,
    idmediopago    NUMBER(2)
)
LOGGING;

ALTER TABLE mediopagotx ADD CONSTRAINT mediopagotx_pk PRIMARY KEY ( idmediopagotx );

CREATE TABLE mesa (
    idmesa        NUMBER(4)
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    descmesa      VARCHAR2(30),
    capacidad     NUMBER(2),
    idestadomesa  NUMBER(2) NOT NULL,
    idlocal       NUMBER(2) NOT NULL
)
LOGGING;

ALTER TABLE mesa ADD CONSTRAINT mesa_pk PRIMARY KEY ( idmesa );

CREATE TABLE orden (
    idorden     NUMBER(10)
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    fechaorden  DATE,
    idreserva   NUMBER(10),
    idestado    NUMBER(2) NOT NULL,
    idempturno  NUMBER(4),
    idmesa      NUMBER(4) NOT NULL
)
LOGGING;

ALTER TABLE orden ADD CONSTRAINT orden_pk PRIMARY KEY ( idorden );

CREATE TABLE pagopedido (
    idcompra     NUMBER(10)
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    fechacompra  DATE,
    idpedido     NUMBER(10) NOT NULL,
    idpersona    NUMBER(10) NOT NULL
)
LOGGING;

ALTER TABLE pagopedido ADD CONSTRAINT pagopedido_pk PRIMARY KEY ( idcompra );

CREATE TABLE pedido (
    idpedido     NUMBER(10)
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    fechapedido  DATE,
    idpersona    NUMBER(10) NOT NULL
)
LOGGING;

ALTER TABLE pedido ADD CONSTRAINT pedido_pk PRIMARY KEY ( idpedido );

CREATE TABLE permisos (
    idpermiso          INTEGER
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    fechamodificacion  DATE,
    idrol              NUMBER(10) NOT NULL,
    idfuncionalidad    NUMBER(4) NOT NULL
)
LOGGING;

ALTER TABLE permisos ADD CONSTRAINT permisos_pk PRIMARY KEY ( idpermiso );

CREATE TABLE persona (
    idpersona        NUMBER(10)
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    rut              NUMBER(8) NOT NULL,
    dv               CHAR(1) NOT NULL,
    nombre           VARCHAR2(50),
    apellidopaterno  VARCHAR2(50),
    apellidomaterno  VARCHAR2(50),
    direccion        VARCHAR2(50),
    fono             VARCHAR2(15),
    fechaingreso     DATE,
    email            VARCHAR2(50)
)
LOGGING;

COMMENT ON COLUMN persona.idpersona IS
    'Auto incrementable';

COMMENT ON COLUMN persona.rut IS
    'identificador unico';

COMMENT ON COLUMN persona.dv IS
    'Dv identificador unico';

COMMENT ON COLUMN persona.nombre IS
    'Descripcion persona';

COMMENT ON COLUMN persona.apellidopaterno IS
    'Apellido Paterno';

COMMENT ON COLUMN persona.apellidomaterno IS
    'Apellido Materno';

COMMENT ON COLUMN persona.direccion IS
    'Direccion Persona';

COMMENT ON COLUMN persona.fono IS
    'Fono Persona';

COMMENT ON COLUMN persona.fechaingreso IS
    'Fecha ingreso persona';

COMMENT ON COLUMN persona.email IS
    'Email Persona';

ALTER TABLE persona ADD CONSTRAINT persona_pk PRIMARY KEY ( idpersona );

ALTER TABLE persona ADD CONSTRAINT rut_un UNIQUE ( rut );

CREATE TABLE prodpreparacion (
    idprodprepar       NUMBER(6)
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    tiempopreparacion  INTEGER NOT NULL,
    idproducto         NUMBER(6),
    idtipoprep         NUMBER(2) NOT NULL,
    dificultad         NUMBER(4)
)
LOGGING;

CREATE UNIQUE INDEX prodpreparacion__idx ON
    prodpreparacion (
        idproducto
    ASC )
        LOGGING;

ALTER TABLE prodpreparacion ADD CONSTRAINT prodpreparacion_pk PRIMARY KEY ( idprodprepar );

CREATE TABLE producto (
    idproducto      NUMBER(6)
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    descproducto    VARCHAR2(100),
    precioproducto  INTEGER,
    idestproducto   NUMBER(2) NOT NULL,
    idtipoproducto  NUMBER(2) NOT NULL,
    idrecproducto   NUMBER(4),
    foto            NVARCHAR2(1000)
)
LOGGING;

CREATE UNIQUE INDEX producto__idx ON
    producto (
        idrecproducto
    ASC )
        LOGGING;

ALTER TABLE producto ADD CONSTRAINT producto_pk PRIMARY KEY ( idproducto );

CREATE TABLE proveedor (
    idproveedor     NUMBER(4)
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    rutproveedor    NUMBER(8) NOT NULL,
    dvproveedor     CHAR(1) NOT NULL,
    nomprov         VARCHAR2(200 CHAR),
    fono            VARCHAR2(10),
    idestproveedor  NUMBER(2) NOT NULL
)
LOGGING;

ALTER TABLE proveedor ADD CONSTRAINT proveedor_pk PRIMARY KEY ( idproveedor );

CREATE TABLE recetaproducto (
    idrecproducto  NUMBER(4)
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    ingrediente    VARCHAR2(1000),
    elaboracion    VARCHAR2(1000)
)
LOGGING;

ALTER TABLE recetaproducto ADD CONSTRAINT recetaproducto_pk PRIMARY KEY ( idrecproducto );

CREATE TABLE reserva (
    idreserva        NUMBER(10)
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    fechareserva     DATE,
    cantidadcliente  NUMBER(4),
    idestadoreserva  NUMBER(2) NOT NULL,
    idpersona        NUMBER(10),
    msgreserva       NVARCHAR2(250),
    horareserva      NVARCHAR2(10)
)
LOGGING;

ALTER TABLE reserva ADD CONSTRAINT reserva_pk PRIMARY KEY ( idreserva );

CREATE TABLE restaurant (
    idlocal         NUMBER(2)
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    direccionlocal  VARCHAR2(100)
)
LOGGING;

ALTER TABLE restaurant ADD CONSTRAINT restaurant_pk PRIMARY KEY ( idlocal );

CREATE TABLE rol (
    idrol           NUMBER(10)
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    descripcionrol  VARCHAR2(100),
    fechacreacion   DATE
)
LOGGING;

ALTER TABLE rol ADD CONSTRAINT rol_pk PRIMARY KEY ( idrol );

CREATE TABLE tipocuenta (
    idtipocuenta    NUMBER(2)
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    desctipocuenta  VARCHAR2(20)
)
LOGGING;

ALTER TABLE tipocuenta ADD CONSTRAINT tipocuenta_pk PRIMARY KEY ( idtipocuenta );

CREATE TABLE tipomovbanco (
    idtipomovbco    NUMBER(2)
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    desctipomovbco  VARCHAR2(30)
)
LOGGING;

ALTER TABLE tipomovbanco ADD CONSTRAINT tipomovbanco_pk PRIMARY KEY ( idtipomovbco );

CREATE TABLE tipomovbodega (
    idtipomovbdga      NUMBER(4)
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    desctipomovbodega  VARCHAR2(50)
)
LOGGING;

ALTER TABLE tipomovbodega ADD CONSTRAINT tipomovbodega_pk PRIMARY KEY ( idtipomovbdga );

CREATE TABLE tipopreparacion (
    idtipoprep    NUMBER(2)
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    desctipoprep  VARCHAR2(30)
)
LOGGING;

COMMENT ON COLUMN tipopreparacion.idtipoprep IS
    'Alta
Media
Baja';

ALTER TABLE tipopreparacion ADD CONSTRAINT tipopreparacion_pk PRIMARY KEY ( idtipoprep );

CREATE TABLE tipoproducto (
    idtipoproducto    NUMBER(2)
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    desctipoproducto  VARCHAR2(50)
)
LOGGING;

ALTER TABLE tipoproducto ADD CONSTRAINT tipoproducto_pk PRIMARY KEY ( idtipoproducto );

CREATE TABLE usuario (
    idpersona  NUMBER(10) NOT NULL,
    userid     VARCHAR2(20) NOT NULL,
    password   NVARCHAR2(256) NOT NULL
)
LOGGING;

COMMENT ON COLUMN usuario.idpersona IS
    'Auto incrementable';

ALTER TABLE usuario ADD CONSTRAINT usuario_pk PRIMARY KEY ( idpersona );

CREATE TABLE usuariorol (
    idusuariorol       NUMBER(10)
        GENERATED ALWAYS AS IDENTITY ( START WITH 1 NOCACHE ORDER )
    NOT NULL,
    fechamodificacion  DATE,
    idrol              NUMBER(10) NOT NULL,
    idpersona          NUMBER(10) NOT NULL
)
LOGGING;

ALTER TABLE usuariorol ADD CONSTRAINT usuariorol_pk PRIMARY KEY ( idusuariorol );

ALTER TABLE bancomovimiento
    ADD CONSTRAINT bcomov_entidadbancaria_fk FOREIGN KEY ( identbancaria )
        REFERENCES entidadbancaria ( identbancaria )
    NOT DEFERRABLE;

ALTER TABLE bancomovimiento
    ADD CONSTRAINT bcomov_tipomovbanco_fk FOREIGN KEY ( idtipomovbco )
        REFERENCES tipomovbanco ( idtipomovbco )
    NOT DEFERRABLE;

ALTER TABLE bodegamovimiento
    ADD CONSTRAINT bgamov_tipomovbodega_fk FOREIGN KEY ( idtipomovbdga )
        REFERENCES tipomovbodega ( idtipomovbdga )
    NOT DEFERRABLE;

ALTER TABLE bodegamovimiento
    ADD CONSTRAINT bodegamov_insumo_fk FOREIGN KEY ( idinsumo )
        REFERENCES insumo ( idinsumo )
    NOT DEFERRABLE;

ALTER TABLE cajaoperacion
    ADD CONSTRAINT cajaoperacion_cajaestado_fk FOREIGN KEY ( idcajaestado )
        REFERENCES cajaestado ( idcajaestado )
    NOT DEFERRABLE;

ALTER TABLE cajaoperacion
    ADD CONSTRAINT cajaoperacion_usuario_fk FOREIGN KEY ( idpersona )
        REFERENCES usuario ( idpersona )
    NOT DEFERRABLE;

ALTER TABLE cliente
    ADD CONSTRAINT cliente_persona_fk FOREIGN KEY ( idpersona )
        REFERENCES persona ( idpersona )
    NOT DEFERRABLE;

ALTER TABLE detalleorden
    ADD CONSTRAINT detalleorden_estadoorden_fk FOREIGN KEY ( idestado )
        REFERENCES estadoorden ( idestado )
    NOT DEFERRABLE;

ALTER TABLE detalleorden
    ADD CONSTRAINT detalleorden_orden_fk FOREIGN KEY ( idorden )
        REFERENCES orden ( idorden )
    NOT DEFERRABLE;

ALTER TABLE detalleorden
    ADD CONSTRAINT detalleorden_producto_fk FOREIGN KEY ( idproducto )
        REFERENCES producto ( idproducto )
    NOT DEFERRABLE;

ALTER TABLE detpedidoins
    ADD CONSTRAINT detpedidoins_insumo_fk FOREIGN KEY ( idinsumo )
        REFERENCES insumo ( idinsumo )
    NOT DEFERRABLE;

ALTER TABLE detpedidoins
    ADD CONSTRAINT detpedidoins_pedido_fk FOREIGN KEY ( idpedido )
        REFERENCES pedido ( idpedido )
    NOT DEFERRABLE;

ALTER TABLE documentopago
    ADD CONSTRAINT docpago_mediopago_fk FOREIGN KEY ( idmediopago )
        REFERENCES mediopago ( idmediopago )
    NOT DEFERRABLE;

ALTER TABLE documentopago
    ADD CONSTRAINT documentopago_cliente_fk FOREIGN KEY ( idpersona )
        REFERENCES cliente ( idpersona )
    NOT DEFERRABLE;

ALTER TABLE documentopago
    ADD CONSTRAINT documentopago_doctpagotipo_fk FOREIGN KEY ( iddoctpagotipo )
        REFERENCES doctpagotipo ( iddoctpagotipo )
    NOT DEFERRABLE;

ALTER TABLE empleadoturno
    ADD CONSTRAINT empleadoturno_usuario_fk FOREIGN KEY ( idpersona )
        REFERENCES usuario ( idpersona )
    NOT DEFERRABLE;

ALTER TABLE entidadbancaria
    ADD CONSTRAINT entidadbancaria_tipocuenta_fk FOREIGN KEY ( idtipocuenta )
        REFERENCES tipocuenta ( idtipocuenta )
    NOT DEFERRABLE;

ALTER TABLE ingrediente
    ADD CONSTRAINT ingrediente_prodpreparacion_fk FOREIGN KEY ( prodpreparacion_idprodprepar )
        REFERENCES prodpreparacion ( idprodprepar )
    NOT DEFERRABLE;

ALTER TABLE insumostock
    ADD CONSTRAINT insumostock_insumo_fk FOREIGN KEY ( idinsumo )
        REFERENCES insumo ( idinsumo )
    NOT DEFERRABLE;

ALTER TABLE mediopagotx
    ADD CONSTRAINT mediopagotx_mediopago_fk FOREIGN KEY ( idmediopago )
        REFERENCES mediopago ( idmediopago )
    NOT DEFERRABLE;

ALTER TABLE mesa
    ADD CONSTRAINT mesa_estadomesa_fk FOREIGN KEY ( idestadomesa )
        REFERENCES estadomesa ( idestadomesa )
    NOT DEFERRABLE;

ALTER TABLE mesa
    ADD CONSTRAINT mesa_restaurant_fk FOREIGN KEY ( idlocal )
        REFERENCES restaurant ( idlocal )
    NOT DEFERRABLE;

ALTER TABLE orden
    ADD CONSTRAINT orden_empleadoturno_fk FOREIGN KEY ( idempturno )
        REFERENCES empleadoturno ( idempturno )
    NOT DEFERRABLE;

ALTER TABLE orden
    ADD CONSTRAINT orden_estadoorden_fk FOREIGN KEY ( idestado )
        REFERENCES estadoorden ( idestado )
    NOT DEFERRABLE;

ALTER TABLE orden
    ADD CONSTRAINT orden_mesa_fk FOREIGN KEY ( idmesa )
        REFERENCES mesa ( idmesa )
    NOT DEFERRABLE;

ALTER TABLE orden
    ADD CONSTRAINT orden_reserva_fk FOREIGN KEY ( idreserva )
        REFERENCES reserva ( idreserva )
    NOT DEFERRABLE;

ALTER TABLE pagopedido
    ADD CONSTRAINT pagopedido_pedido_fk FOREIGN KEY ( idpedido )
        REFERENCES pedido ( idpedido )
    NOT DEFERRABLE;

ALTER TABLE pagopedido
    ADD CONSTRAINT pagopedido_usuario_fk FOREIGN KEY ( idpersona )
        REFERENCES usuario ( idpersona )
    NOT DEFERRABLE;

ALTER TABLE pedido
    ADD CONSTRAINT pedido_usuario_fk FOREIGN KEY ( idpersona )
        REFERENCES usuario ( idpersona )
    NOT DEFERRABLE;

ALTER TABLE permisos
    ADD CONSTRAINT permisos_funcionalidad_fk FOREIGN KEY ( idfuncionalidad )
        REFERENCES funcionalidad ( idfuncionalidad )
    NOT DEFERRABLE;

ALTER TABLE permisos
    ADD CONSTRAINT permisos_rol_fk FOREIGN KEY ( idrol )
        REFERENCES rol ( idrol )
    NOT DEFERRABLE;

ALTER TABLE prodpreparacion
    ADD CONSTRAINT prodprep_producto_fk FOREIGN KEY ( idproducto )
        REFERENCES producto ( idproducto )
    NOT DEFERRABLE;

ALTER TABLE prodpreparacion
    ADD CONSTRAINT prodprep_tipopreparacion_fk FOREIGN KEY ( idtipoprep )
        REFERENCES tipopreparacion ( idtipoprep )
    NOT DEFERRABLE;

ALTER TABLE producto
    ADD CONSTRAINT producto_estadoproducto_fk FOREIGN KEY ( idestproducto )
        REFERENCES estadoproducto ( idestproducto )
    NOT DEFERRABLE;

ALTER TABLE producto
    ADD CONSTRAINT producto_recetaproducto_fk FOREIGN KEY ( idrecproducto )
        REFERENCES recetaproducto ( idrecproducto )
    NOT DEFERRABLE;

ALTER TABLE producto
    ADD CONSTRAINT producto_tipoproducto_fk FOREIGN KEY ( idtipoproducto )
        REFERENCES tipoproducto ( idtipoproducto )
    NOT DEFERRABLE;

ALTER TABLE proveedor
    ADD CONSTRAINT proveedor_estadoproveedor_fk FOREIGN KEY ( idestproveedor )
        REFERENCES estadoproveedor ( idestproveedor )
    NOT DEFERRABLE;

ALTER TABLE reserva
    ADD CONSTRAINT reserva_cliente_fk FOREIGN KEY ( idpersona )
        REFERENCES cliente ( idpersona )
    NOT DEFERRABLE;

ALTER TABLE reserva
    ADD CONSTRAINT reserva_estadoreserva_fk FOREIGN KEY ( idestadoreserva )
        REFERENCES estadoreserva ( idestadoreserva )
    NOT DEFERRABLE;

ALTER TABLE usuario
    ADD CONSTRAINT usuario_persona_fk FOREIGN KEY ( idpersona )
        REFERENCES persona ( idpersona )
    NOT DEFERRABLE;

ALTER TABLE usuariorol
    ADD CONSTRAINT usuariorol_rol_fk FOREIGN KEY ( idrol )
        REFERENCES rol ( idrol )
    NOT DEFERRABLE;

ALTER TABLE usuariorol
    ADD CONSTRAINT usuariorol_usuario_fk FOREIGN KEY ( idpersona )
        REFERENCES usuario ( idpersona )
    NOT DEFERRABLE;



-- Informe de Resumen de Oracle SQL Developer Data Modeler: 
-- 
-- CREATE TABLE                            46
-- CREATE INDEX                             4
-- ALTER TABLE                             91
-- CREATE VIEW                              0
-- ALTER VIEW                               0
-- CREATE PACKAGE                           0
-- CREATE PACKAGE BODY                      0
-- CREATE PROCEDURE                         0
-- CREATE FUNCTION                          0
-- CREATE TRIGGER                           0
-- ALTER TRIGGER                            0
-- CREATE COLLECTION TYPE                   0
-- CREATE STRUCTURED TYPE                   0
-- CREATE STRUCTURED TYPE BODY              0
-- CREATE CLUSTER                           0
-- CREATE CONTEXT                           0
-- CREATE DATABASE                          0
-- CREATE DIMENSION                         0
-- CREATE DIRECTORY                         0
-- CREATE DISK GROUP                        0
-- CREATE ROLE                              0
-- CREATE ROLLBACK SEGMENT                  0
-- CREATE SEQUENCE                          0
-- CREATE MATERIALIZED VIEW                 0
-- CREATE MATERIALIZED VIEW LOG             0
-- CREATE SYNONYM                           0
-- CREATE TABLESPACE                        0
-- CREATE USER                              0
-- 
-- DROP TABLESPACE                          0
-- DROP DATABASE                            0
-- 
-- REDACTION POLICY                         0
-- 
-- ORDS DROP SCHEMA                         0
-- ORDS ENABLE SCHEMA                       0
-- ORDS ENABLE OBJECT                       0
-- 
-- ERRORS                                   0
-- WARNINGS                                 0
