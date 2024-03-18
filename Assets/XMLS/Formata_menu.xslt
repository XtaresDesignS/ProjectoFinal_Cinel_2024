<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="xml" indent="yes"/>

  <xsl:template match="/">
    <xsl:choose>
      <xsl:when test="data/user/perfil ='Admin'">
        <a class="dropdown-item" href="/Pages/FristBackend.aspx">Dashboard</a>
        <a class="dropdown-item" href="/Pages/Carrega_Coins.aspx">Novas Coins</a>
        <hr/>
      </xsl:when>
      <xsl:otherwise>
        <xsl:choose>
          <xsl:when test="data/user/perfil='RH'">
            <a class="dropdown-item" href="/Pages/FristBackend.aspx">Dashboard</a>
            <a class="dropdown-item" href="/Pages/Carrega_Coins.aspx">Novas Coins</a>
          </xsl:when>
          <xsl:otherwise>
            <xsl:choose>
              <xsl:when test="data/user/perfil = 'Formador'">
                <a class="dropdown-item" href="/Pages/alterar.aspx">Alterar Palavra-Passe</a>
                <a class="dropdown-item" href="/Pages/Collect.aspx?id={data/user/id_Utilizador}">Minhas Colleções de Coins</a>
                <hr/>
              </xsl:when>
              <xsl:otherwise>
                <xsl:choose>
                  <xsl:when test="data/user/perfil = 'Formando'">
                    <a class="dropdown-item" href="/Pages/alterar.aspx">Alterar Palavra-Passe</a>
                    <a class="dropdown-item" href="/Pages/Collect.aspx?id={data/user/id_Utilizador}">Minhas Colleções de Coins</a>
                    <hr/>
                  </xsl:when>
                  <xsl:otherwise>
                    <a class="dropdown-item" href="/Pages/Login.aspx">Login</a>
                    <a class="dropdown-item" href="/Pages/Regist.aspx">Registo</a>
                  </xsl:otherwise>
                </xsl:choose>

              </xsl:otherwise>
            </xsl:choose>

          </xsl:otherwise>
        </xsl:choose>

      </xsl:otherwise>
    </xsl:choose>

  </xsl:template>
</xsl:stylesheet>
