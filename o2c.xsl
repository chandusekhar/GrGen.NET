<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
	<xsl:output method="text"/>
	<xsl:template match="/unit">
<xsl:text>
#include <gr/gr.h>
#include "o2c.h"

/**
 * Fills in the grs_ids structure. 
 * @param graph The ids are created with respect to this graph.
 * @param grs_ids This structure will be filled in. 
 */
void grs_init_ids(gr_graph_t* graph, grs_ids_t *grs_ids) {

/* NODES */

</xsl:text>
		<xsl:for-each select="node_type">
			<xsl:text>	grs_ids->N_</xsl:text>
			<xsl:value-of select="@name"/>
			<xsl:text> = 
		gr_get_node_type_id(graph, "</xsl:text>
			<xsl:value-of select="@name"/>
			<xsl:text>");</xsl:text>
<xsl:text>
</xsl:text>
		</xsl:for-each>
<xsl:text>

/* NODE ATTRIBUTES */

</xsl:text>
			<xsl:for-each select="node_type/attributes/entity">
			<xsl:text>	grs_ids->NA_</xsl:text>
			<xsl:value-of select="../../@name"/>
			<xsl:text>__</xsl:text>
			<xsl:value-of select="@name"/>
			<xsl:text> = 
		gr_get_node_attr_id(graph, grs_ids->N_</xsl:text>
			<xsl:value-of select="../../@name"/>
			<xsl:text>, "</xsl:text>
			<xsl:value-of select="@name"/>
			<xsl:text>");</xsl:text>
<xsl:text>
</xsl:text>
		</xsl:for-each>
<xsl:text>

/* EDGES */

</xsl:text>
		<xsl:for-each select="edge_type">
			<xsl:text>	grs_ids->E_</xsl:text>
			<xsl:value-of select="@name"/>
			<xsl:text> = 
		gr_get_edge_type_id(graph, "</xsl:text>
			<xsl:value-of select="@name"/>
			<xsl:text>");</xsl:text>
<xsl:text>
</xsl:text>
		</xsl:for-each>
<xsl:text>

/* EDGE ATTRIBUTES */

</xsl:text>
		<xsl:for-each select="edge_type/attributes/entity">
			<xsl:text>	grs_ids->EA_</xsl:text>
			<xsl:value-of select="../../@name"/>
			<xsl:text>__</xsl:text>
			<xsl:value-of select="@name"/>
			<xsl:text> = 
		gr_get_edge_attr_id(graph, grs_ids->E_</xsl:text>
			<xsl:value-of select="../../@name"/>
			<xsl:text>, "</xsl:text>
			<xsl:value-of select="@name"/>
			<xsl:text>");</xsl:text>
<xsl:text>
</xsl:text>
		</xsl:for-each>
<xsl:text>
}
</xsl:text>

<!--
<xsl:text>

/* ENUMS */

</xsl:text>
		<xsl:for-each select="enum_type">
			<xsl:text>	gr_id_t T_</xsl:text>
			<xsl:value-of select="@name"/>
<xsl:text>
</xsl:text>
		</xsl:for-each>
}
-->

	</xsl:template>
</xsl:stylesheet>
