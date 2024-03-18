<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="xml" indent="yes"/>

  <xsl:template match="/">

    <xsl:choose>
      <xsl:when test="data/user/perfil = 'Admin'">
        <img class="Avatar" width="40px" heigth="40px" style=" border-radius:50%;" src="{data/user/imagem}" />
        <a
            class="dropdown-toggle"
            href="#"
            id="dropdownId"
            data-toggle="dropdown"
            aria-haspopup="true"
            aria-expanded="false">
          <xsl:value-of select="data/user/nome"></xsl:value-of>
        </a>
      </xsl:when>
      <xsl:otherwise>
        <xsl:choose>
          <xsl:when test="data/user/perfil = 'RH'">
            <img class="Avatar" width="40px" heigth="40px" style=" border-radius:50%;" src="{data/user/imagem}" />
            <a
                class="dropdown-toggle"
                href="#"
                id="dropdownId"
                data-toggle="dropdown"
                aria-haspopup="true"
                aria-expanded="false">
              <xsl:value-of select="data/user/nome"></xsl:value-of>
            </a>
          </xsl:when>
          <xsl:otherwise>
            <xsl:choose>
              <xsl:when test="data/user/perfil = 'Formador'">
                <img class="Avatar" width="40px" heigth="40px" style=" border-radius:50%;" src="{data/user/imagem}" />
                <a
                    class="dropdown-toggle"
                    href="#"
                    id="dropdownId"
                    data-toggle="dropdown"
                    aria-haspopup="true"
                    aria-expanded="false">
                  <xsl:value-of select="data/user/nome"></xsl:value-of>
                </a>
              </xsl:when>
              <xsl:otherwise>
                <xsl:choose>
                  <xsl:when test="data/user/perfil = 'Formando'">
                    <img class="Avatar" width="40px" heigth="40px" style=" border-radius:50%;" src="{data/user/imagem}" />

                    <a
                        class="dropdown-toggle"
                        href="#"
                        id="dropdownId"
                        data-toggle="dropdown"
                        aria-haspopup="true"
                        aria-expanded="false">
                      <xsl:value-of select="data/user/nome"></xsl:value-of>
                    </a>
                  </xsl:when>
                  <xsl:otherwise>
                    <a
                   class="nav-link dropdown-toggle"
                   href="#"
                   id="dropdownId"
                   data-toggle="dropdown"
                   aria-haspopup="false"
                   aria-expanded="false"
            >
                      <lord-icon
                        src="https://cdn.lordicon.com/bgebyztw.json"
                        trigger="in"
                        delay="2000"
                        state="hover-looking-around"
                        style="width: 30px; height: 30px"
              >
                      </lord-icon>
                      <span> Utilizador</span>
                    </a>
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
